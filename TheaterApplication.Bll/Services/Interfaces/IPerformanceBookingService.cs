using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheaterApplication.Bll.Services.Interfaces
{
    public interface IPerformanceBookingService
    {
        Task<int> CreateAsync(int scheduleId, int posterId);
    }
}
