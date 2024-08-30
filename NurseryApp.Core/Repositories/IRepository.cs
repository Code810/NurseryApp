using System.Linq.Expressions;

namespace NurseryApp.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> FindWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdWithIncludesAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate = null);

    }
}
