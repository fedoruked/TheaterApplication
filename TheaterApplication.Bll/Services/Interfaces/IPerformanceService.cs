using System.Threading.Tasks;
using TheaterApplication.Bll.Models;

namespace TheaterApplication.Bll.Services.Interfaces
{
    public interface IPerformanceService
    {
        Task<int> CreateAsync(Performance performance);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, Performance performance);
        Task<Performance[]> GetAllAsync();
    }
}
