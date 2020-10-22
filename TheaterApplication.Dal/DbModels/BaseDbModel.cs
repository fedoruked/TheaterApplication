using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheaterApplication.Dal.DbModels
{
    public abstract class BaseDbModel<T> where T: struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public BaseDbModel()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }
    }
}
