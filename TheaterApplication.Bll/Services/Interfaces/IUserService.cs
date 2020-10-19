using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheaterApplication.Bll.Models;

namespace TheaterApplication.Bll.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserWithTokenData> LoginAsync(string email, string password);
        Task<User> CreateAsync(string email, string password);
        Task<UserWithTokenData> ApproveAsync(string email, Guid? code);
    }
}
