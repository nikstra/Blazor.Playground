using System;
using System.Collections.ObjectModel;
using System.Linq;
using WebUi.Models;

namespace WebUi.Pages.Components.Calendar
{
    public interface ICalendarModel
    {
        ObservableCollection<CalendarEntry> Entries { get; }
        ILookup<DateOnly, PublicHolidayModel> Holidays { get; }
    }
}