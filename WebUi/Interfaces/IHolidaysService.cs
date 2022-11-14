using System;
using System.Linq;
using System.Threading.Tasks;
using WebUi.Models;

namespace WebUi.Interfaces
{
    public interface IHolidaysService
    {
        Task<ILookup<DateOnly, PublicHolidayModel>> GetPublicHolidaysAsync(int year, string countryCode);
    }
}