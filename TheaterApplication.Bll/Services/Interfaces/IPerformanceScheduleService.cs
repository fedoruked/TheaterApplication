using System.Threading.Tasks;
using TheaterApplication.Bll.Models;

namespace TheaterApplication.Bll.Services.Interfaces
{
    public interface IPerformanceScheduleService
    {
        Task<int> CreateAsync(PerformanceSchedule schedule);
        Task DeleteAsync(int id);
        Task UpdateAsync(PerformanceSchedule schedule);
    }
}
