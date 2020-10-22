using System.Threading.Tasks;
using TheaterApplication.Bll.Exceptions;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.Bll.Storages.Interfaces;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Bll.Services
{
    public class PerformanceBookingService: IPerformanceBookingService
    {
        private readonly IPerformanceBookingRepository _performanceBookingRepository;
        private readonly IPerformancePosterRepository _performancePosterRepository;
        private readonly IPerformanceScheduleRepository _performanceScheduleRepository;

        private readonly IApplicationStorage _appStorage;

        public PerformanceBookingService(IPerformanceBookingRepository performanceBookingRepository,
            IPerformancePosterRepository performancePosterRepository,
            IPerformanceScheduleRepository performanceScheduleRepository,

            IApplicationStorage appStorage)
        {
            _performanceBookingRepository = performanceBookingRepository;
            _performancePosterRepository = performancePosterRepository;
            _performanceScheduleRepository = performanceScheduleRepository;

            _appStorage = appStorage;
        }

        public async Task<int> CreateAsync(int scheduleId, int posterId)
        {
            var poster = await _performancePosterRepository.
                FindAsync(posterId);

            if(poster == null || poster.ScheduleId != scheduleId)
            {
                throw new InternalHandlingException("PBC.01", "Poster not found");
            }

            var schedule = await _performanceScheduleRepository.
                FindAsync(scheduleId);

            var countBookings = await _performanceBookingRepository.
                CountByPosterIdAsync(posterId); 

            if(schedule.TicketsCount <= countBookings)
            {
                throw new InternalHandlingException("PBC.02", "No tickets");
            }

            var bookingDb = new PerformanceBookingDbModel();
            bookingDb.PosterId = posterId;
            bookingDb.UserId = _appStorage.User.Id;

            await _performanceBookingRepository.InsertAsync(bookingDb);

            return bookingDb.Id;
        }
    }
}
