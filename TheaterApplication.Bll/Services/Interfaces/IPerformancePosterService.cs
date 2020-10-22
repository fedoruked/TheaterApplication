using System;
using System.Threading.Tasks;
using TheaterApplication.Bll.Models;
using TheaterApplication.Utils;

namespace TheaterApplication.Bll.Services.Interfaces
{
    public interface IPerformancePosterService
    {
        Task<DataWithPaging<PerformancePoster>> GetPageAsync(int? page, int? pageSize,
            string keyword, DateTime? fromDate, DateTime? toDate);
        Task<int> CreateAsync(int performanceId,
            int scheduleId, PerformancePoster poster);
    }
}
