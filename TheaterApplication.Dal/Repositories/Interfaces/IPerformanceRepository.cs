using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;

namespace TheaterApplication.Dal.Repositories.Interfaces
{
    public interface IPerformanceRepository: IBaseRepository<PerformanceDbModel>
    {
        Task<PerformanceDbModel[]> GetAllAsync();
    }
}
