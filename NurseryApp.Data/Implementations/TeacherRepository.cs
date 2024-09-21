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
    }
}
