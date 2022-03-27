using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    public class Week
    {
        public IEnumerable<Day> Days { get; set; }
        public int WeekNumber { get; set; }
    }
}