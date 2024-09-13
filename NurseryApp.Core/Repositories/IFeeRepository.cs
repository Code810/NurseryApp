using NurseryApp.Core.Entities;
using System.Linq.Expressions;

namespace NurseryApp.Core.Repositories
{
    public interface IFeeRepository : IRepository<Fee>
    {
        Task<Fee> GetLaastFeeAsync(Expression<Func<Fee, bool>> predicate);
    }
}
