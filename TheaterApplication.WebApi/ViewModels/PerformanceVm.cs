namespace TheaterApplication.WebApi.ViewModels
{
    public class PerformanceVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }

        public PerformanceScheduleVm[] Schedules { get; set; }
    }
}
