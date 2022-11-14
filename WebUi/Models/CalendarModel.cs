using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WebUi.Pages.Components.Calendar;

namespace WebUi.Models
{
    public class CalendarModel : ICalendarModel
    {
        public CalendarModel(IEnumerable<CalendarEntry> entries, ILookup<DateOnly, PublicHolidayModel> holidays)
        {
            Entries = new ObservableCollection<CalendarEntry>(entries);
            Holidays = holidays;
        }

        // TODO: Do we need this to be an ObservableCollection?
        public ObservableCollection<CalendarEntry> Entries { get; private set; }
        public ILookup<DateOnly, PublicHolidayModel> Holidays { get; private set; }
    }
}