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
                 // Same name can be used in different regions. But, should we filter that here?
                .DistinctBy(h => h.LocalName, StringComparer.InvariantCultureIgnoreCase)
                .Select(h => h.LocalName)
                .ToArray();
        // TODO: Optimize logic for properties. Rename properties paying attention to css class names.
        public bool CalendarHolidayListIndicator => Holidays.Count() > 2;
        public bool Holiday => Holidays.Any();
    }
}