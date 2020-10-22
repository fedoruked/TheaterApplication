using AutoMapper;
using System;
using System.Threading.Tasks;
using TheaterApplication.Bll.Exceptions;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;
using TheaterApplication.Utils;

namespace TheaterApplication.Bll.Services
{
    public class PerformancePosterService: IPerformancePosterService
    {
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 10;
        private readonly DateTime MinFromDate = DateTime.UtcNow;
        private readonly DateTime MaxToDate = DateTime.UtcNow.AddYears(1);

        private readonly IPerformancePosterRepository _performancePosterRepository;
        private readonly IPerformanceScheduleRepository _performanceScheduleRepository;
        private readonly IMapper _mapper;

        public PerformancePosterService(IPerformancePosterRepository performancePosterRepository,
            IPerformanceScheduleRepository performanceScheduleRepository,
            IMapper mapper)
        {
            _performancePosterRepository = performancePosterRepository;
            _performanceScheduleRepository = performanceScheduleRepository;
            _mapper = mapper;
        }

        public async Task<DataWithPaging<PerformancePoster>> GetPageAsync(int? page, int? pageSize, 
            string keyword, DateTime? fromDate, DateTime? toDate)
        {
            page = page.HasValue ? page : DefaultPage;
            pageSize = pageSize.HasValue ? pageSize : DefaultPageSize;
            keyword = !string.IsNullOrEmpty(keyword) ? keyword : string.Empty;
            fromDate = fromDate.HasValue ? fromDate : MinFromDate;
            toDate = toDate.HasValue && toDate < MaxToDate ? toDate : MaxToDate;


            var resultDb = await _performancePosterRepository.GetPageAsync(
                page.Value, pageSize.Value, keyword, fromDate.Value, toDate.Value);

            var result = new DataWithPaging<PerformancePoster>();
            result.Data = _mapper.Map<PerformancePoster[]>(resultDb.Data);
            result.Page = resultDb.Page;
            result.TotalCount = resultDb.TotalCount;
            result.PageSize = resultDb.PageSize;

            return result;
        }

        public async Task<int> CreateAsync(int performanceId,
            int scheduleId, PerformancePoster poster)
        {
            poster.ScheduleId = scheduleId;

            var schedule = await _performanceScheduleRepository.
                FindAsync(scheduleId);

            if (schedule == null || schedule.PerformanceId != performanceId)
            {
                throw new InternalHandlingException("PPC.01", "Schedule not found");
            }

            var posterDb = _mapper.Map<PerformancePosterDbModel>(poster);
            await _performancePosterRepository.InsertAsync(posterDb);

            return posterDb.Id;
        }
    }
}
