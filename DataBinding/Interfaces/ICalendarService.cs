using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBinding.Models;
using DataBinding.Pages.Components.Calendar;

namespace DataBinding.Interfaces
{
    public interface ICalendarService
    {
        Task<IEnumerable<CalendarEntry>> GetEntriesAsync(DateOnly date);
    }
}