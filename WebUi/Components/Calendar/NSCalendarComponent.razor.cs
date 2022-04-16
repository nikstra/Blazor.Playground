using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebUi.Extensions;
using WebUi.Interfaces;
using WebUi.Models;

namespace WebUi.Components.Calendar
{
    public partial class NSCalendarComponent
    {
        public const string Today = "today";
        public const string More = "more";

        private const int EntryLimit = 3;

        [Inject] ICalendarService CalendarService { get; set; }
        [Inject] IHolidaysService HolidaysService { get; set; }
        [Inject] IStringLocalizer<NSCalendarComponent> Localizer { get; set; }
        [Inject] IJSRuntime JsRuntime { get; set; }

        private string GetFormattedMonthName(int month) =>
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetMonthName(month).FirstCharToUpper();
        private string GetLocalTimeString(DateTimeOffset date) =>
            date.ToString(Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern);

        [Parameter]
        public DateOnly SelectedDate
        {
            get => _selectedDate;
            set
            {
                PreviousMonth = value.AddMonths(-1);
                NextMonth = value.AddMonths(1);
                _selectedDate = value;
            }
        }
        private DateOnly _selectedDate = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly PreviousMonth { get; set; }
        public DateOnly NextMonth { get; set; }
        
        private static System.Timers.Timer _timer;
        public string _todayPulse;
        private void RunPulseTodayAnimation()
        {
            if(_timer is not null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }

            _todayPulse = "pulse";
            _timer = new System.Timers.Timer(500);
            _timer.Elapsed += (sender, args) =>
            {
                _todayPulse = string.Empty;
                InvokeAsync(StateHasChanged);
            };
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        private async Task GotoToday()
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            if(SelectedDate == now)
            {
                RunPulseTodayAnimation();
                return;
            }

            SelectedDate = now;
            await LoadCalendar();
        }

        private async Task GotoPrevious()
        {
            SelectedDate = PreviousMonth;
            await LoadCalendar();
        }
        private async Task GotoNext()
        {
            SelectedDate = NextMonth;
            await LoadCalendar();
        }
        private void ShowDetails(DateOnly date, MouseEventArgs eventArgs)
        {
            var dayEntries = _entries[date];
            if(!dayEntries.Any())
            {
                return;
            }
            // TODO: Add a details view.
            var texts = dayEntries.Select(e => $"{e.Start} {e.Duration} {e.Name}");

            JsRuntime.InvokeVoidAsync("alert", "CALENDAR ENTRIES" + Environment.NewLine + string.Join(Environment.NewLine, texts));
        }

        private string GetCssClass(DateOnly date)
        {
            if(DateOnly.FromDateTime(DateTime.Today) == date)
            {
                return "today";
            }
            else if(SelectedDate.Month != date.Month)
            {
                return "other-month";
            }

            return string.Empty;
        }

        protected async override Task OnInitializedAsync()
        {
            await LoadCalendar();
            await base.OnInitializedAsync();
        }

        private ILookup<DateOnly, CalendarEntry> _entries;
        private async Task LoadCalendar()
        {
            _entries = (await CalendarService.GetEntriesAsync(SelectedDate))
                .ToLookup(e => DateOnly.FromDateTime(e.Start.DateTime));

            var region = new RegionInfo(Thread.CurrentThread.CurrentCulture.LCID);
            var holidays = await HolidaysService.GetPublicHolidays(SelectedDate.Year, region.TwoLetterISORegionName);

            var startDate = new DateOnly(SelectedDate.Year, SelectedDate.Month, 1);
            Weeks = GetDaysWithEntries(startDate, _entries, holidays);
        }

        public IEnumerable<Week> Weeks { get; set; } = Array.Empty<Week>();

        private IEnumerable<Week> GetDaysWithEntries(
            DateOnly startDate,
            ILookup<DateOnly, CalendarEntry> entries,
            ILookup<DateOnly, PublicHolidayModel> holidays)
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            var firstDayInMonth = startDate;
            var calendarStartDay = -(7 + firstDayInMonth.DayOfWeek - firstDayOfWeek) % 7;

            var uiStartDay = firstDayInMonth.AddDays(calendarStartDay);

            var dayIndex = uiStartDay;
            while(dayIndex < firstDayInMonth.AddMonths(1))
            {
                var week = new Day[7];
                for(var i = 0; i < week.Length; i++)
                {
                    week[i] = new Day
                    {
                        DayNumber = dayIndex.Day,
                        Date = dayIndex,
                        Entries = entries[dayIndex].OrderBy(e => e.Start),
                        Name = culture.DateTimeFormat.DayNames[(int)dayIndex.DayOfWeek],
                        // TODO: There can be more than one holiday for a given date. Just grab the first one for now.
                        Holiday = holidays[dayIndex]?.FirstOrDefault()
                    };
                    dayIndex = dayIndex.AddDays(1);
                }
                
                var weekNumber = culture.Calendar.GetWeekOfYear(
                    week[0].Date.ToDateTime(TimeOnly.MaxValue),
                    culture.DateTimeFormat.CalendarWeekRule,
                    firstDayOfWeek);

                yield return new Week { Days = week, WeekNumber = weekNumber };
            }
        }

        public IEnumerable<string> WeekDays { get; set; } = GetWeekDays();
        private static IEnumerable<string> GetWeekDays(string cultureName = null)
        {
            var culture = cultureName is null
                ? Thread.CurrentThread.CurrentCulture
                : CultureInfo.CreateSpecificCulture(cultureName);
            var formatInfo = culture.DateTimeFormat;
            var firstDayOfWeek = (int)formatInfo.FirstDayOfWeek;
            var firstDayOffset = 7 - firstDayOfWeek;

            var namesShifted = new string[formatInfo.DayNames.Length];
            Array.Copy(formatInfo.DayNames, firstDayOfWeek, namesShifted, 0, firstDayOffset);
            Array.Copy(formatInfo.DayNames, 0, namesShifted, firstDayOffset, firstDayOfWeek);

            return namesShifted;
        }
    }
}