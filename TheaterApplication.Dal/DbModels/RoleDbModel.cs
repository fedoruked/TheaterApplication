using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("roles")]
    public class RoleDbModel: BaseDbModel<byte>
    {
        [MaxLength(64)]
        public string Name { get; set; }

        public virtual ICollection<UserRoleDbModel> UserRoles { get; set; }
    }
}
