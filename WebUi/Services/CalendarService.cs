using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUi.Interfaces;
using WebUi.Models;

namespace WebUi.Services
{
    public class CalendarService : ICalendarService
    {
        public Task<IEnumerable<CalendarEntry>> GetEntriesAsync(DateOnly date)
        {
            // return Task.FromResult(Array.Empty<CalendarEntry>().AsEnumerable());
            var now = DateTimeOffset.Now;
            var startOfMonth = new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, now.Offset);
            return Task.FromResult(new List<CalendarEntry>
            {
                new CalendarEntry
                {
                    Name = "Meeting 1",
                    Start = startOfMonth.Add(new TimeSpan(4, 10, 0, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 2",
                    Start = startOfMonth.Add(new TimeSpan(14, 13, 10, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 3",
                    Start = startOfMonth.Add(new TimeSpan(14, 10, 0, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 4",
                    Start = startOfMonth.Add(new TimeSpan(14, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 5",
                    Start = startOfMonth.Add(new TimeSpan(21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 6",
                    Start = startOfMonth.Add(new TimeSpan(21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 7",
                    Start = startOfMonth.Add(new TimeSpan(21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CalendarEntry
                {
                    Name = "Meeting 8",
                    Start = startOfMonth.Add(new TimeSpan(21, 11, 15, 0)),
                    Duration = TimeSpan.FromMinutes(45)
                },
            }.AsEnumerable());
        }
    }
}