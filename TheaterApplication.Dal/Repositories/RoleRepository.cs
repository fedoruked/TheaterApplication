using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public class RoleRepository : BaseRepository<RoleDbModel>, IRoleRepository
    {
        public RoleRepository(TheaterDbContext dbContext)
            : base(dbContext) { }
    }
}
