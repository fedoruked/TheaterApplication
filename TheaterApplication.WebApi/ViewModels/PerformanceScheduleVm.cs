using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterApplication.WebApi.ViewModels
{
    public class PerformanceScheduleVm
    {
        public int Id { get; set; }
        public DateTime StartAt { get; set; }
        public int TicketsCount { get; set; }
        public bool IsRepeat { get; set; }

        public PerformanceVm Performance { get; set; }
    }
}
