using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheaterApplication.Bll.Exceptions;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Bll.Services
{
    public class PerformanceScheduleService : IPerformanceScheduleService
    {
        private readonly IPerformanceScheduleRepository _performanceScheduleRepository;
        private readonly IPerformanceRepository _performanceRepository;
        private readonly IMapper _mapper;

        public PerformanceScheduleService(IPerformanceScheduleRepository performanceScheduleRepository,
            IPerformanceRepository performanceRepository,
            IMapper mapper)
        {
            _performanceScheduleRepository = performanceScheduleRepository;
            _performanceRepository = performanceRepository;

            _mapper = mapper;
        }

        public async Task<int> CreateAsync(PerformanceSchedule schedule)
        {
            var performanceDb = await _performanceRepository.
                FindAsync(schedule.PerformanceId);

            if (performanceDb == null)
            {
                throw new InternalHandlingException("PSC.01",
                    "Performance not found");
            }

            var scheduleDb = _mapper.Map<PerformanceScheduleDbModel>(schedule);
            await _performanceScheduleRepository.InsertAsync(scheduleDb);

            return scheduleDb.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var scheduleDb = await _performanceScheduleRepository.
                FindAsync(id);

            if (scheduleDb == null)
            {
                throw new InternalHandlingException("PSD.01",
                    "Schedule not found");
            }

            await _performanceScheduleRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(PerformanceSchedule schedule)
        {
            var scheduleDb = await _performanceScheduleRepository.
                FindAsync(schedule.Id);

            if (scheduleDb == null)
            {
                throw new InternalHandlingException("PSD.01",
                    "Schedule not found");
            }

            if (scheduleDb.StartAt != schedule.StartAt 
                || scheduleDb.TicketsCount != schedule.TicketsCount 
                || scheduleDb.IsRepeat != schedule.IsRepeat)
            {
                scheduleDb.StartAt = schedule.StartAt;
                scheduleDb.TicketsCount = schedule.TicketsCount;
                scheduleDb.IsRepeat = schedule.IsRepeat;
                scheduleDb.Updated = DateTime.UtcNow;

                await _performanceScheduleRepository.UpdateAsync(scheduleDb);
            }
        }
    }
}
