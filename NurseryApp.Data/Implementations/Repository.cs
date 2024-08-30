using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;
using System.Linq.Expressions;

namespace NurseryApp.Data.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly NurseryAppContext _context;
        private readonly DbSet<TEntity> _table;

        public Repository(NurseryAppContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _table.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _table.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetAllIncludes(includes);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetAllIncludes(includes).Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdWithIncludesAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetAllIncludes(includes);
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<bool> IsExist(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? await _table.AnyAsync() : await _table.AnyAsync(predicate);
        }

        public IQueryable<TEntity> GetAllIncludes(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _table;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
