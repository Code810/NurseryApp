using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;
using System.Linq.Expressions;

namespace NurseryApp.Data.Implementations
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        protected readonly NurseryAppContext _context;
        public GroupRepository(NurseryAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Group>> GetAllAsyncWithSorting(Expression<Func<Group, bool>> predicate, int? count = null, params Expression<Func<Group, object>>[] includes)
        {
            IQueryable<Group> query = _context.Groups.Where(predicate).OrderByDescending(b => b.CreatedDate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }
            return await query.ToListAsync();
        }
    }
}
