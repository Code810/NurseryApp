using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class ParentRepository : Repository<Parent>, IParentRepository
    {
        protected readonly NurseryAppContext _context;

        public ParentRepository(NurseryAppContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Parent> GetByAppUserId(string id)
        {
            return await _context.Parents
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.AppUserId == id && !p.IsDeleted);
        }
    }
}
