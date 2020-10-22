using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("user_roles")]
    public class UserRoleDbModel: BaseDbModel<int>
    {
        public int UserId { get; set; }
        public byte RoleId { get; set; }

        public virtual UserDbModel User { get; set; }
        public virtual RoleDbModel Role { get; set; }
    }
}
