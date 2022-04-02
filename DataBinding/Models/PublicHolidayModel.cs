using System;
using System.Collections.Generic;

namespace DataBinding.Models
{
    public class PublicHolidayModel
    {
        public DateTime Date { get; set; }
        public string LocalName { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public bool Fixed { get; set; }
        public bool Global { get; set; }
        public IEnumerable<string> Countries { get; set; }
        public int? LaunchYear { get; set; }
        public IEnumerable<PublicHolidayType> Types { get; set; }
    }
}