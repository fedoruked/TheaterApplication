using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.WebApi.Authorization;
using TheaterApplication.WebApi.PostModels;
using TheaterApplication.WebApi.ViewModels;

namespace TheaterApplication.WebApi.Controllers
{
    [Route("performances")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceService _performanceService;
        private readonly IMapper _mapper;

        public PerformanceController(IPerformanceService performanceService,
            IMapper mapper)
        {
            _performanceService = performanceService;
            _mapper = mapper;
        }

        [HttpPut]
        [AuthByToken("admin")]
        public async Task<int> CreateAsync(PerformancePm performancePm)
        {
            var performance = _mapper.Map<Performance>(performancePm);
            var id = await _performanceService.CreateAsync(performance);

            return id;
        }

        [HttpDelete]
        [AuthByToken("admin")]
        [Route("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _performanceService.DeleteAsync(id);
        }

        [HttpPost]
        [AuthByToken("admin")]
        public async Task UpdateAsync(int id, [FromBody] PerformancePm performancePm)
        {
            var performance = _mapper.Map<Performance>(performancePm);
            await _performanceService.UpdateAsync(id, performance);
        }

        [HttpGet]
        [AuthByToken("admin")]
        public async Task<PerformanceVm[]> GetAllAsync()
        {
            var performances = await _performanceService.GetAllAsync();
            var performancesVm = _mapper.Map<PerformanceVm[]>(performances);

            return performancesVm;
        }

    }
}
