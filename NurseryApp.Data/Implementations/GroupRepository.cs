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

        public async Task<int> GetAllCount(string? text)
        {

            IQueryable<Group> query = _context.Groups.Where(g => !g.IsDeleted);
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.Name.ToLower().Contains(text.ToLower()) || b.Language.ToLower().Contains(text.ToLower()));

            return query.Count();

        }

        public async Task<IEnumerable<Group>> GetAllWithSearch(string? text, int page = 1, params Expression<Func<Group, object>>[] includes)
        {
            IQueryable<Group> query = _context.Groups.Where(b => !b.IsDeleted).OrderByDescending(b => b.CreatedDate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            if (!string.IsNullOrEmpty(text)) query = query.Where(b => b.Name.ToLower().Contains(text.ToLower()) || b.Language.ToLower().Contains(text.ToLower()));
            IEnumerable<Group> groups = await query.Skip((page - 1) * 9).Take(9).ToListAsync();
            return groups;
        }
        public async Task<Group> GetGroupById(int groupId)
        {
            return await _context.Groups
                .Include(g => g.Teacher)
                .Include(g => g.Students).ThenInclude(s => s.Parent)
                .FirstOrDefaultAsync(g => g.Id == groupId);
        }
    }
}
