using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("performance_bookings")]
    public class PerformanceBookingDbModel: BaseDbModel<int>
    {
        public int PosterId { get; set; }
        public int UserId { get; set; }

        public virtual PerformancePosterDbModel Poster { get; set; }
        public virtual UserDbModel User { get; set; }
    }
}
