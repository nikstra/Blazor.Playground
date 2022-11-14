using System;

namespace WebUi.Models
{
    public class CalendarEntry
    {
        public int Id { get; }
        public string Name { get; set; }
        public DateTimeOffset Start { get; set; }
        public TimeSpan Duration { get; set; }
        public string DurationString => Duration.ToString("g");
        public DateTimeOffset End => Start.Add(Duration);
    }
}