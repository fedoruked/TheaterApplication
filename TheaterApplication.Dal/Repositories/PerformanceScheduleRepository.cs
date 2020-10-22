using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public class PerformanceScheduleRepository : BaseRepository<PerformanceScheduleDbModel>, IPerformanceScheduleRepository
    {
        public PerformanceScheduleRepository(TheaterDbContext dbContext) 
            : base(dbContext) { }

        public override async Task DeleteAsync(object id)
        {
            var schedule = await _dbContext.PerformanceSchedules.
                Include(x => x.Posters)
                    .ThenInclude(x => x.Bookings).
                FirstOrDefaultAsync(x => x.Id == (int)id);

            _dbContext.PerformanceBookings.RemoveRange(
                schedule.Posters.SelectMany(x => x.Bookings));

            _dbContext.PerformancePosters.RemoveRange(schedule.Posters);

            _dbContext.PerformanceSchedules.Remove(schedule);

            await _dbContext.SaveChangesAsync();
        }
    }
}
