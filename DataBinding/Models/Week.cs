using System.Collections.Generic;

namespace DataBinding.Models
{
    public class Week
    {
        public IEnumerable<Day> Days { get; set; }
        public int WeekNumber { get; set; }
    }
}