using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterApplication.Bll.Services.Interfaces;
using TheaterApplication.WebApi.PostModels;
using TheaterApplication.WebApi.ViewModels;

namespace TheaterApplication.WebApi.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<UserVm> LoginAsync([FromBody] UserPm user)
        {
            var resultUser = await _userService.LoginAsync(user.Email, user.Password);

            var result = _mapper.Map<UserVm>(resultUser);
            return result;
        }

        [HttpPost]
        [Route("register")]
        public async Task<UserVm> CreateAsync([FromBody] UserPm user)
        {
            var resultUser = await _userService.CreateAsync(user.Email, user.Password);

            var result = _mapper.Map<UserVm>(resultUser);
            return result;
        }

        [HttpPost]
        [Route("approve")]
        public async Task<UserVm> ApproveAsync([FromBody] ApproveUserPm approveCode)
        {
            var resultUser = await _userService.ApproveAsync(
                approveCode.Email, approveCode.ApproveCode);

            var result = _mapper.Map<UserVm>(resultUser);
            return result;
        }
    }
}
