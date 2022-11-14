using System;
using System.Threading.Tasks;
using WebUi.Pages.Components.Calendar;

namespace WebUi.Interfaces
{
    public interface ICalendarProvider
    {
        Task<ICalendarModel> GetAsync(DateOnly selectedDate);
    }
}