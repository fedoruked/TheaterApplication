using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public class UserRepository: BaseRepository<UserDbModel>, IUserRepository
    {
        public UserRepository(TheaterDbContext dbContext) 
            : base(dbContext) { }

        public async Task<UserDbModel> FindByEmailAsync(string email)
        {
            var result = await _dbContext.Users.
                Include(x => x.UserRoles).
                    ThenInclude(x => x.Role).
                FirstOrDefaultAsync(x => x.Email == email);

            return result;
        }
    }
}
