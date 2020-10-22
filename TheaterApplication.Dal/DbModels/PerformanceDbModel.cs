using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("performances")]
    public class PerformanceDbModel: BaseDbModel<int>
    {
        public string Name { get; set; }
        public int DurationMinutes { get; set; }

        public virtual ICollection<PerformanceScheduleDbModel> Schedules { get; set; }
    }
}
