using System;
using System.Collections.Generic;

namespace WebUi.Models
{
    public class Day
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int DayNumber { get; set; }
        public IEnumerable<CalendarEntry> Entries { get; set; }
        public PublicHolidayModel Holiday { get; set; }
    }
}