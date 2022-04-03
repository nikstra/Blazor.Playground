using System;
using System.Linq;
using System.Threading.Tasks;
using DataBinding.Models;

namespace DataBinding.Interfaces
{
    public interface IHolidaysService
    {
        Task<ILookup<DateOnly, PublicHolidayModel>> GetPublicHolidays(int year, string countryCode);
    }
}