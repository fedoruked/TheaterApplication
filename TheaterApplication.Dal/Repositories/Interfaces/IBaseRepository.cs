using System.Threading.Tasks;

namespace TheaterApplication.Dal.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        Task<T> FindAsync(object id);
        Task<T> UpdateAsync(T entity);
        Task<T> InsertAsync(T entity);
        Task DeleteAsync(object id);
    }
}
