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

        public async Task<IEnumerable<Blog>> GetAllWithSearch(string? text, int page = 1)
        {
            IQueryable<Blog> query = _context.Blogs.Where(b => !b.IsDeleted).OrderByDescending(b => b.CreatedDate);
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.Title.ToLower().Contains(text.ToLower()) || b.Desc.ToLower().Contains(text.ToLower()));
            IEnumerable<Blog> blogs = await query.Skip((page - 1) * 9).Take(9).ToListAsync();
            return blogs;
        }
        public async Task<int> GetAllCount(string? text)
        {
            IQueryable<Blog> query = _context.Blogs.Where(b => !b.IsDeleted);
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.Title.ToLower().Contains(text.ToLower()) || b.Desc.ToLower().Contains(text.ToLower()));

            return query.Count();
        }
    }
}
