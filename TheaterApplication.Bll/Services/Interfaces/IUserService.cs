using System;
using System.Threading.Tasks;
using TheaterApplication.Bll.Models;
using TheaterApplication.Bll.Models.Enums;

namespace TheaterApplication.Bll.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserWithTokenData> LoginAsync(string email, string password);
        Task<User> CreateAsync(string email, string password);
        Task<UserWithTokenData> ApproveAsync(string email, Guid? code);
        AuthorizationResultEnum Authorization(string token, string[] roles);
    }
}
