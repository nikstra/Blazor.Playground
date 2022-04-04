using System;
using System.Linq;
using System.Threading.Tasks;
using WebUi.Models;

namespace WebUi.Interfaces
{
    public interface IHolidaysService
    {
        Task<ILookup<DateOnly, PublicHolidayModel>> GetPublicHolidays(int year, string countryCode);
    }
}