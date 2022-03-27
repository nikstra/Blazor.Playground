using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DataBinding.Models;

namespace DataBinding.Pages.Components.Calendar
{
    public interface ICalendarModel
    {
        ObservableCollection<CalendarEntry> Entries { get; }
    }
}