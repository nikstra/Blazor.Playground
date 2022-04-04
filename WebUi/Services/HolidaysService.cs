using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUi.Clients;
using WebUi.Interfaces;
using WebUi.Models;

namespace WebUi.Services
{
    public class HolidaysService : IHolidaysService
    {
        private readonly HolidaysClient _httpClient;
        private static readonly Dictionary<(int, string), ILookup<DateOnly, PublicHolidayModel>> _holidays = new();

        public HolidaysService(HolidaysClient client)
        {
            _httpClient = client;
        }

        public async Task<ILookup<DateOnly, PublicHolidayModel>> GetPublicHolidays(int year, string countryCode)
        {
            if(_holidays.TryGetValue((year, countryCode), out var holidaysMap))
            {
                return holidaysMap;
            }

            var holidayList = await _httpClient.GetHolidays(year, countryCode);
            var result = holidayList
                .ToLookup(h => DateOnly.FromDateTime(h.Date));

            _holidays[(year, countryCode)] = result;

            return result;
        }
    }
}