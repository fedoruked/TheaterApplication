using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("performance_schedules")]
    public class PerformanceScheduleDbModel: BaseDbModel<int>
    {
        public int PerformanceId { get; set; }
        public DateTime StartAt { get; set; }
        public int TicketsCount { get; set; }
        public bool IsRepeat { get; set; }

        public virtual PerformanceDbModel Performance { get; set; }
        public virtual ICollection<PerformancePosterDbModel> Posters { get; set; }
    }
}
