using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.WebApi.Authorization;
using TheaterApplication.WebApi.PostModels;

namespace TheaterApplication.WebApi.Controllers
{
    [Route("performances/{performanceId}/schedules")]
    [ApiController]
    public class PerformanceScheduleController : ControllerBase
    {
        private readonly IPerformanceScheduleService _performanceScheduleService;
        private readonly IMapper _mapper;

        public PerformanceScheduleController(IPerformanceScheduleService performanceScheduleService,
            IMapper mapper)
        {
            _performanceScheduleService = performanceScheduleService;
            _mapper = mapper;
        }

        [HttpPut]
        [AuthByToken("admin")]
        public async Task<int> CreateAsync(int performanceId, [FromBody] PerformanceSchedulePm schedulePm)
        {
            var schedule = _mapper.Map<PerformanceSchedule>(schedulePm);
            schedule.PerformanceId = performanceId;

            var id = await _performanceScheduleService.CreateAsync(schedule);

            return id;
        }

        [HttpDelete]
        [AuthByToken("admin")]
        [Route("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _performanceScheduleService.DeleteAsync(id);
        }

        [HttpPost]
        [AuthByToken("admin")]
        public async Task UpdateAsync(int performanceId, [FromBody] PerformanceSchedulePm schedulePm)
        {
            var schedule = _mapper.Map<PerformanceSchedule>(schedulePm);
            schedule.PerformanceId = performanceId;

            await _performanceScheduleService.UpdateAsync(schedule);
        }
    }
}
