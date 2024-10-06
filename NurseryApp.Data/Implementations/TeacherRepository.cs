using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;
using System.Linq.Expressions;

namespace NurseryApp.Data.Implementations
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        protected readonly NurseryAppContext _context;
        public TeacherRepository(NurseryAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsyncWithSorting(Expression<Func<Teacher, bool>> predicate, int? count = null, params Expression<Func<Teacher, object>>[] includes)
        {
            IQueryable<Teacher> query = _context.Teachers.Where(predicate).OrderByDescending(b => b.CreatedDate);
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

        public async Task<IEnumerable<Teacher>> GetAllWithSearch(string? text, int page = 1)
        {
            IQueryable<Teacher> query = _context.Teachers.Where(b => !b.IsDeleted).OrderByDescending(b => b.CreatedDate);
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.FirstName.ToLower().Contains(text.ToLower()) || b.LastName.ToLower().Contains(text.ToLower()));
            IEnumerable<Teacher> teachers = await query.Skip((page - 1) * 9).Take(9).ToListAsync();
            return teachers;
        }
        public async Task<int> GetAllCount(string? text)
        {
            IQueryable<Teacher> query = _context.Teachers.Where(b => !b.IsDeleted);
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.FirstName.ToLower().Contains(text.ToLower()) || b.LastName.ToLower().Contains(text.ToLower()));
            return query.Count();
        }

        public async Task<Teacher> GetByAppUserId(string id)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(p => p.AppUserId == id && !p.IsDeleted);
        }
    }
}
