namespace TheaterApplication.Bll.Models
{
    public class Performance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }

        public PerformanceSchedule[] Schedules { get; set; }
    }
}
