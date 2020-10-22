using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.WebApi.Authorization;
using TheaterApplication.WebApi.PostModels;

namespace TheaterApplication.WebApi.Controllers
{
    [Route("performances/{performanceId}/schedules/{scheduleId}/posters/{posterId}/bookings")]
    [ApiController]
    public class PerformanceBookingController : ControllerBase
    {
        private readonly IPerformanceBookingService _performanceBookingService;

        public PerformanceBookingController(IPerformanceBookingService performanceBookingService)
        {
            _performanceBookingService = performanceBookingService;
        }

        [HttpPut]
        [AuthByToken("client,admin")]
        public async Task<int> CreateAsync(int scheduleId, int posterId)
        {
            var id = await _performanceBookingService.
                CreateAsync(scheduleId, posterId);

            return id;
        }
    }
}
