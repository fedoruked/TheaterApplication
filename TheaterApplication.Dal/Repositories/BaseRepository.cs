using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TheaterApplication.Dal.Repositories.Interfaces;

namespace TheaterApplication.Dal.Repositories
{
    public abstract class BaseRepository<T>: IBaseRepository<T> where T: class
    {
        protected readonly TheaterDbContext _dbContext;

        public BaseRepository(TheaterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> FindAsync(object id)
        {
            var result = await _dbContext.Set<T>().FindAsync(id);

            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await FindAsync(id);

            if(entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
