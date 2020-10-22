using System;
using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Utils;

namespace TheaterApplication.Dal.Repositories.Interfaces
{
    public interface IPerformancePosterRepository: IBaseRepository<PerformancePosterDbModel>
    {
        Task<DataWithPaging<PerformancePosterDbModel>> GetPageAsync(
            int page, int pageSize, string keyword, DateTime fromDate, DateTime toDate);
    }
}
