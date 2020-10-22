using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    [Table("tokens")]
    public class TokenDbModel: BaseDbModel<int>
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime? Expired { get; set; }

        public virtual UserDbModel User { get; set; }
    }
}
