using System;
using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;

namespace TheaterApplication.Dal.Repositories.Interfaces
{
    public interface IUserRepository: IBaseRepository<UserDbModel>
    {
        Task<UserDbModel> FindByEmailAsync(string email);
    }
}
