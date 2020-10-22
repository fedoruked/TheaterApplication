using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;

namespace TheaterApplication.Dal.Repositories.Interfaces
{
    public interface IPerformanceBookingRepository: IBaseRepository<PerformanceBookingDbModel>
    {
        Task<int> CountByPosterIdAsync(int posterId);
    }
}
