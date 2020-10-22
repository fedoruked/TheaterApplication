using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.Utils;
using TheaterApplication.WebApi.Authorization;
using TheaterApplication.WebApi.PostModels;
using TheaterApplication.WebApi.ViewModels;

namespace TheaterApplication.WebApi.Controllers
{
    [Route("performances")]
    [ApiController]
    public class PerformancePosterController : ControllerBase
    {
        private readonly IPerformancePosterService _performancePosterService;
        private readonly IMapper _mapper;

        public PerformancePosterController(IPerformancePosterService performancePosterService,
            IMapper mapper)
        {
            _performancePosterService = performancePosterService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("posters")]
        public async Task<DataWithPaging<PerformancePosterVm>> GetPageAsync(int? page, int? pageSize,
            string keyword, DateTime? fromDate, DateTime? toDate)
        {
            var postersWithPaging = await _performancePosterService.GetPageAsync(
                page, pageSize, keyword, fromDate, toDate);

            // implement mapping
            var result = new DataWithPaging<PerformancePosterVm>();
            result.Data = _mapper.Map<PerformancePosterVm[]>(postersWithPaging.Data);
            result.Page = postersWithPaging.Page;
            result.TotalCount = postersWithPaging.TotalCount;
            result.PageSize = postersWithPaging.PageSize;

            return result;
        }

        [HttpPut]
        [AuthByToken("client,admin")]
        [Route("{performanceId}/schedules/{scheduleId}/posters")]
        public async Task<int> CreateAsync(int performanceId, int scheduleId, 
            [FromBody]PerformancePosterPm posterPm)
        {
            var poster = _mapper.Map<PerformancePoster>(posterPm);
            var id = await _performancePosterService.CreateAsync(
                performanceId, scheduleId, poster);

            return id;
        }
    }
}
