using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;
using System.Linq.Expressions;

namespace NurseryApp.Data.Implementations
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {

        protected readonly NurseryAppContext _context;
        public BlogRepository(NurseryAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllAsyncWithSorting(Expression<Func<Blog, bool>> predicate, int? count = null)
        {
            IQueryable<Blog> query = _context.Blogs.Where(predicate).OrderByDescending(b => b.CreatedDate);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }
            return await query.ToListAsync();
        }
    }
}
