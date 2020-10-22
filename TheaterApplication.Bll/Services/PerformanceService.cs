using AutoMapper;
using System;
using System.Threading.Tasks;
using TheaterApplication.Bll.Exceptions;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Bll.Services
{
    public class PerformanceService: IPerformanceService
    {
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IMapper _mapper;

        public PerformanceService(IPerformanceRepository performanceRepository,
            IMapper mapper)
        {
            _performanceRepository = performanceRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Performance performance)
        {
            var performanceDb = _mapper.Map<PerformanceDbModel>(performance);
            await _performanceRepository.InsertAsync(performanceDb);

            return performanceDb.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var performanceDb = await _performanceRepository.FindAsync(id);

            if(performanceDb == null)
            {
                throw new InternalHandlingException("PD.01", 
                    "Performance not found");
            }

            await _performanceRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(int id, Performance performance)
        {
            var performanceDb = await _performanceRepository.
                FindAsync(id);

            if(performanceDb == null)
            {
                throw new InternalHandlingException("UD.01",
                    "Performance not found");
            }

            if(performanceDb.Name != performance.Name 
                || performanceDb.DurationMinutes != performance.DurationMinutes)
            {
                performanceDb.Name = performance.Name;
                performanceDb.DurationMinutes = performance.DurationMinutes;
                performanceDb.Updated = DateTime.UtcNow;

                await _performanceRepository.UpdateAsync(performanceDb);
            }
        }

        public async Task<Performance[]> GetAllAsync()
        {
            var performancesDb = await _performanceRepository.GetAllAsync();
            var performances = _mapper.Map<Performance[]>(performancesDb);

            return performances;
        }
    }
}
