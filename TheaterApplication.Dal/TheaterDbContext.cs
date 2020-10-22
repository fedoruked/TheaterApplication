using Microsoft.EntityFrameworkCore;
using System;
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
        public DbSet<PerformanceDbModel> Performances { get; set; }
        public DbSet<PerformanceScheduleDbModel> PerformanceSchedules { get; set; }
        public DbSet<PerformancePosterDbModel> PerformancePosters { get; set; }
        public DbSet<PerformanceBookingDbModel> PerformanceBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDbModel>().
                HasIndex(x => x.Email).
                IsUnique();

            modelBuilder.Entity<UserRoleDbModel>().
                HasIndex(x => new { x.UserId, x.RoleId }).
                IsUnique();

            modelBuilder.Entity<PerformancePosterDbModel>().
                HasIndex(x => new { x.ScheduleId, x.DifferenceFromStartDays }).
                IsUnique();

            modelBuilder.Entity<PerformanceScheduleDbModel>().
                HasIndex(x => x.StartAt);

            modelBuilder.Entity<PerformanceDbModel>().
                HasIndex(x => x.Name);

            base.OnModelCreating(modelBuilder);
        }
    }
}