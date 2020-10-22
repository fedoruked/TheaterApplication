using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("users")]
    public class UserDbModel: BaseDbModel<int>
    {
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(64)]
        public string Password { get; set; }
        public Guid? ApproveCode { get; set; }
        public DateTime? Approved { get; set; }

        public virtual ICollection<UserRoleDbModel> UserRoles { get; set; }
        public virtual ICollection<TokenDbModel> Tokens { get; set; }
        public virtual ICollection<PerformanceBookingDbModel> Bookings { get; set; }
    }
}
