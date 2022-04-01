using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBinding.Interfaces;
using DataBinding.Models;

namespace DataBinding.Services
{
    public class CalendarService : ICalendarService
    {
        public Task<IEnumerable<CalendarEntry>> GetEntriesAsync(DateOnly date)
        {
            // return Task.FromResult(Array.Empty<CalendarEntry>().AsEnumerable());
            return Task.FromResult(new List<CalendarEntry>
            {
                new CalendarEntry
                {
                    Name = "Meeting 1",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 4, 10, 0, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 2",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 14, 13, 10, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 3",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 14, 10, 0, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 4",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 14, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 5",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 6",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 7",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 8",
                    Start = new DateTimeOffset(new DateTime(2022, 4, 21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
            }.AsEnumerable());
        }
    }
}