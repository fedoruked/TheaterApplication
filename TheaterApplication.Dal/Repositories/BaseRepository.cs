using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public virtual async Task<T> FindAsync(object id)
        {
            var result = await _dbContext.Set<T>().FindAsync(id);

            return result;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> entities)
        {
            foreach(var e in entities)
            {
                _dbContext.Entry(e).State = EntityState.Added;
            }

            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task DeleteAsync(object id)
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
