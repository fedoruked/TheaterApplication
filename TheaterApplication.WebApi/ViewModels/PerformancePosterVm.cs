using System;

namespace TheaterApplication.WebApi.ViewModels
{
    public class PerformancePosterVm
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public int DifferenceFromStartDays { get; set; }
        public long? BookedCount { get; set; }
        public long FreeTickets { get
            {
                long result = 0;

                if (Schedule != null)
                {
                    result = Schedule.TicketsCount - BookedCount ?? 0;
                }

                return result;
            }
        }

        public PerformanceScheduleVm Schedule { get; set; }
    }
}
