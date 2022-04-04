using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUi.Models;
using WebUi.Pages.Components.Calendar;

namespace WebUi.Interfaces
{
    public interface ICalendarService
    {
        Task<IEnumerable<CalendarEntry>> GetEntriesAsync(DateOnly date);
    }
}