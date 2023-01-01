using System;
using System.Collections.Generic;
using System.Linq;
using WebUi.Models;

namespace WebUi.Components.Calendar.Models
{
    public class Day
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int DayNumber { get; set; }
        public IEnumerable<CalendarEntry> Entries { get; set; } = Enumerable.Empty<CalendarEntry>();
        public IEnumerable<PublicHolidayModel> Holidays { get; set; } = Enumerable.Empty<PublicHolidayModel>();
        public string[] GetHolidayLocalNames() =>
            Holidays.Where(h => !string.IsNullOrWhiteSpace(h.LocalName))
                .Select(h => h.LocalName)
                .ToArray();

    }
}