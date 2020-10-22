using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public class PerformanceBookingRepository : BaseRepository<PerformanceBookingDbModel>, IPerformanceBookingRepository
    {
        public PerformanceBookingRepository(TheaterDbContext dbContext)
            : base(dbContext) { }

        public async Task<int> CountByPosterIdAsync(int posterId)
        {
            var result = await _dbContext.PerformanceBookings.
                CountAsync(x => x.PosterId == posterId);

            return result;
        }
    }
}
