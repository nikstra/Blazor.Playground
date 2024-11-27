using System;
using System.Collections.Generic;

namespace WebUi.Models
{
    public class PublicHolidayModel
    {
        public DateTime Date { get; set; }
        public string LocalName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public bool Fixed { get; set; }
        public bool Global { get; set; }
        public IEnumerable<string> Countries { get; set; } = Enumerable.Empty<string>();
        public int? LaunchYear { get; set; }
        public IEnumerable<PublicHolidayType> Types { get; set; } = Enumerable.Empty<PublicHolidayType>();
    }
}