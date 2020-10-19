using Microsoft.EntityFrameworkCore;
using TheaterApplication.Dal.DbModels;

namespace TheaterApplication.Dal
{
    public class TheaterDbContext : DbContext
    {
        public TheaterDbContext(DbContextOptions options) 
            : base(options) { }

        public DbSet<RoleDbModel> Roles { get; set; }
        public DbSet<TokenDbModel> Tokens { get; set; }
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<UserRoleDbModel> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDbModel>().
                HasIndex(x => x.Email).
                IsUnique();

            modelBuilder.Entity<UserRoleDbModel>().
                HasIndex(x => new { x.UserId, x.RoleId }).
                IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
