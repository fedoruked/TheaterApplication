using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheaterApplication.Dal.Builders.Interfaces;
using TheaterApplication.Dal.DbModels;
using TheaterApplication.Dal.Repositories.Interfaces;
using TheaterApplication.Utils;

namespace TheaterApplication.Dal.Repositories
{
    public class PerformancePosterRepository : BaseRepository<PerformancePosterDbModel>, IPerformancePosterRepository
    {
        private readonly IParametersBuilder _parametersBuilder;

        public PerformancePosterRepository(TheaterDbContext dbContext,
            IParametersBuilder parametersBuilder) : base(dbContext)
        {
            _parametersBuilder = parametersBuilder;
        }

        public async Task<DataWithPaging<PerformancePosterDbModel>> GetPageAsync(
            int page, int pageSize, string keyword, DateTime fromDate, DateTime toDate)
        {
            var parameters = _parametersBuilder.
                Add("@from_date", fromDate).
                Add("@to_date", toDate).
                Build();

            var query = _dbContext.PerformancePosters.
                FromSqlRaw("select * from generate_posters(@from_date, @to_date)", parameters);

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = $"%{keyword}%";
                query = query.Where(x => EF.Functions.Like(x.Schedule.Performance.Name, keyword));
            }

            var totalCount = query.Count();

            query = query.
                OrderBy(x => x.EventDate).
                Skip((page - 1) * pageSize).
                Take(pageSize);

            var posters = await query.
                Include(x => x.Schedule).
                    ThenInclude(x => x.Performance).
                AsNoTracking().
                ToArrayAsync();

            var result = new DataWithPaging<PerformancePosterDbModel>();
            result.Data = posters;
            result.TotalCount = totalCount;
            result.Page = page;
            result.PageSize = pageSize;

            return result;
        }
    }
}
