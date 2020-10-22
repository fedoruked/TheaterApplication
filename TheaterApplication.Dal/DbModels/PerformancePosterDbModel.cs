using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("performance_posters")]
    public class PerformancePosterDbModel: BaseDbModel<int>
    {
        public int ScheduleId { get; set; }
        public DateTime EventDate { get; set; }
        public int DifferenceFromStartDays { get; set; }
        public long? BookedCount { get; set; }

        public virtual PerformanceScheduleDbModel Schedule { get; set; }
        public virtual ICollection<PerformanceBookingDbModel> Bookings { get; set; }
    }
}
