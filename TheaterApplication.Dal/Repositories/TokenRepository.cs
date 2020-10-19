using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public class TokenRepository: BaseRepository<TokenDbModel>, ITokenRepository
    {
        public TokenRepository(TheaterDbContext dbContext)
            : base(dbContext) { }
    }
}
