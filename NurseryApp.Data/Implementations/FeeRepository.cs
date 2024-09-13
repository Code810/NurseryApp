using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;
using System.Linq.Expressions;

namespace NurseryApp.Data.Implementations
{
    public class FeeRepository : Repository<Fee>, IFeeRepository
    {
        protected readonly NurseryAppContext _context;
        public FeeRepository(NurseryAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Fee> GetLaastFeeAsync(Expression<Func<Fee, bool>> predicate)
        {
            return await _context.Fees.OrderByDescending(f => f.DueDate).FirstOrDefaultAsync(predicate);
        }
    }
}
