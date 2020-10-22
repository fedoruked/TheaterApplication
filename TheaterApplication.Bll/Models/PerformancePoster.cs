using System;

namespace TheaterApplication.Bll.Models
{
    public class PerformancePoster
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTime EventDate { get; set; }
        public int DifferenceFromStartDays { get; set; }
        public long? BookedCount { get; set; }

        public PerformanceSchedule Schedule { get; set; }
    }
}
