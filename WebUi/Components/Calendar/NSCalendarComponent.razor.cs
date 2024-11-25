using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using WebUi.Components.Calendar.Models;
using WebUi.Extensions;
using WebUi.Interfaces;
using WebUi.Models;

namespace WebUi.Components.Calendar
{
    public partial class NSCalendarComponent
    {
        private const int EntryLimit = 3;
        public const string Today = "today";
        public const string More = "more";

        private static Timer _timer;
        private string _todayPulse;
        private bool _showEntries;
        private ILookup<DateOnly, CalendarEntry> _entries;
        private IEnumerable<Week> Weeks { get; set; } = Array.Empty<Week>();
        private IEnumerable<string> WeekDays { get; set; } = GetLocalizedDayNames();
        private DateOnly PreviousMonth { get; set; }
        private DateOnly NextMonth { get; set; }
        private DateOnly _selectedDate = DateOnly.FromDateTime(DateTime.Now);
        [Parameter] public DateOnly SelectedDate { get; set; }

        [Inject] ICalendarProvider CalendarProvider { get; set; }
        [Inject] IStringLocalizer<NSCalendarComponent> Localizer { get; set; }
        [Inject] IJSRuntime JsRuntime { get; set; }

        protected async override Task OnInitializedAsync()
        {
            UpdateSelectedDate(SelectedDate);
            await LoadCalendar();
            await base.OnInitializedAsync();
        }

        private void UpdateSelectedDate(DateOnly date)
        {
            _selectedDate = date;
            PreviousMonth = _selectedDate.AddMonths(-1);
            NextMonth = _selectedDate.AddMonths(1);
        }

        private async Task LoadCalendar()
        {
            var calendarModel = await CalendarProvider.GetAsync(_selectedDate);
            _entries = calendarModel.Entries.ToLookup(e => DateOnly.FromDateTime(e.Start.DateTime));

            var startDate = new DateOnly(_selectedDate.Year, _selectedDate.Month, 1);
            Weeks = GetDaysWithEntries(startDate, _entries, calendarModel.Holidays);
        }

        private string GetFormattedMonthName(int month) =>
            CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).FirstCharToUpper();
        
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
            if(_selectedDate == now)
            {
                RunPulseTodayAnimation();
                return;
            }

            UpdateSelectedDate(now);
            await LoadCalendar();
        }

        private async Task GotoPrevious()
        {
            UpdateSelectedDate(PreviousMonth);
            await LoadCalendar();
        }

        private async Task GotoNext()
        {
            UpdateSelectedDate(NextMonth);
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

            // JsRuntime.InvokeVoidAsync("alert", "CALENDAR ENTRIES" + Environment.NewLine + string.Join(Environment.NewLine, texts));
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

        private static IEnumerable<Week> GetDaysWithEntries(
            DateOnly startDate,
            ILookup<DateOnly, CalendarEntry> entries,
            ILookup<DateOnly, PublicHolidayModel> holidays)
        {
            var culture = CultureInfo.CurrentCulture;
            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            var firstDayInMonth = startDate;
            var calendarStartDay = -(7 + firstDayInMonth.DayOfWeek - firstDayOfWeek) % 7;

            var dayIndex = firstDayInMonth.AddDays(calendarStartDay);
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
                        Holidays = holidays[dayIndex]
                    };
                    dayIndex = dayIndex.AddDays(1);
                }
                
                var weekNumber = GetWeekNumber(week[0].Date);

                yield return new Week { Days = week, WeekNumber = weekNumber };
            }
        }

        private static int GetWeekNumber(DateOnly date) =>
            CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                date.ToDateTime(TimeOnly.MaxValue),
                CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule,
                CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        private static IEnumerable<string> GetLocalizedDayNames()
        {
            var formatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            var firstDayOfWeek = (int)formatInfo.FirstDayOfWeek;

            for(var i = 0; i < 7; i++)
            {
                yield return formatInfo.DayNames[(i + firstDayOfWeek) % 7];
            }
        }
    }
}