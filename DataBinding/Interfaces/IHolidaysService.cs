using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBinding.Models;

namespace DataBinding.Interfaces
{
    public interface IHolidaysService
    {
        Task<IDictionary<DateOnly, PublicHolidayModel>> GetPublicHolidays(int year, string countryCode);
    }
}