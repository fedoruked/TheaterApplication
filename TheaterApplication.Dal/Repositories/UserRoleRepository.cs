using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public class UserRoleRepository: BaseRepository<UserRoleDbModel>, IUserRoleRepository
    {
        public UserRoleRepository(TheaterDbContext dbContext)
            : base(dbContext) { }
    }
}
