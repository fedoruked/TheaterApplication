using System;

namespace TheaterApplication.Bll.Models
{
    public class PerformanceSchedule
    {
        public int Id { get; set; }
        public int PerformanceId { get; set; }
        public DateTime StartAt { get; set; }
        public int TicketsCount { get; set; }
        public bool IsRepeat { get; set; }

        public Performance Performance { get; set; }
    }
}
