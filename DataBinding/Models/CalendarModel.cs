using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DataBinding.Models;
using DataBinding.Pages.Components;
using DataBinding.Pages.Components.Calendar;

namespace DataBinding.Models
{
    public class CalendarModel : ICalendarModel
    {
        public ObservableCollection<CalendarEntry> Entries { get; private set; }
    }
}