using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBinding.Clients;
using DataBinding.Interfaces;
using DataBinding.Models;

namespace DataBinding.Services
{
    public class HolidaysService : IHolidaysService
    {
        private readonly HolidaysClient _httpClient;
        private static readonly Dictionary<(int, string), IDictionary<DateOnly, PublicHolidayModel>> _holidays = new();

        public HolidaysService(HolidaysClient client)
        {
            _httpClient = client;
        }

        public async Task<IDictionary<DateOnly, PublicHolidayModel>> GetPublicHolidays(int year, string countryCode)
        {
            if(_holidays.TryGetValue((year, countryCode), out var holidaysMap))
            {
                return holidaysMap;
            }

            var holidayList = await _httpClient.GetHolidays(year, countryCode);
            var result = holidayList
                .ToDictionary(h => DateOnly.FromDateTime(h.Date));

            _holidays[(year, countryCode)] = result;

            return result;
        }
    }
}