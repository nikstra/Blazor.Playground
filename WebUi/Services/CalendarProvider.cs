using System;
using System.Globalization;
using System.Threading.Tasks;
using WebUi.Interfaces;
using WebUi.Models;
using WebUi.Pages.Components.Calendar;

namespace WebUi.Services
{
    public class CalendarProvider : ICalendarProvider
    {
        private readonly ICalendarService _calendarService;
        private readonly IHolidaysService _holidaysService;

        public CalendarProvider(ICalendarService calendarService, IHolidaysService holidaysService)
        {
            _calendarService = calendarService;
            _holidaysService = holidaysService;
        }

        public async Task<ICalendarModel> GetAsync(DateOnly selectedDate)
        {
            var entries = await _calendarService.GetEntriesAsync(selectedDate);
            var region = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            var holidays = await _holidaysService.GetPublicHolidaysAsync(selectedDate.Year, region.TwoLetterISORegionName);

            return new CalendarModel(entries, holidays);
        }
    }
}