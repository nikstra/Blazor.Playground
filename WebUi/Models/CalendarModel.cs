using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WebUi.Models;
using WebUi.Pages.Components;
using WebUi.Pages.Components.Calendar;

namespace WebUi.Models
{
    public class CalendarModel : ICalendarModel
    {
        public ObservableCollection<CalendarEntry> Entries { get; private set; }
    }
}