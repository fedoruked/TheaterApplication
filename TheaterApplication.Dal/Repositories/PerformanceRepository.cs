using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public class PerformanceRepository : BaseRepository<PerformanceDbModel>, IPerformanceRepository
    {
        public PerformanceRepository(TheaterDbContext dbContext) 
            : base(dbContext) { }

        public async Task<PerformanceDbModel[]> GetAllAsync()
        {
            var result = await _dbContext.Performances.
                Include(x => x.Schedules).
                ToArrayAsync();

            return result;
        }

        public override async Task DeleteAsync(object id)
        {
            var performance = await _dbContext.Performances.
                Include(x => x.Schedules).
                    ThenInclude(x => x.Posters).
                        ThenInclude(x => x.Bookings).
                FirstOrDefaultAsync(x => x.Id == (int)id);

            //remove bookings
            var bookings = performance.Schedules.
                SelectMany(x => x.Posters.SelectMany(x => x.Bookings));

            _dbContext.PerformanceBookings.RemoveRange(bookings);

            //remove posters
            _dbContext.PerformancePosters.RemoveRange(
                performance.Schedules.SelectMany(x => x.Posters));

            //remove schedules
            _dbContext.PerformanceSchedules.RemoveRange(performance.Schedules);

            //remove performance
            _dbContext.Performances.Remove(performance);

            await _dbContext.SaveChangesAsync();
        }

    }
}
