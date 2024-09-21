using NurseryApp.Core.Entities;
using System.Linq.Expressions;

namespace NurseryApp.Core.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllAsyncWithSorting(Expression<Func<Blog, bool>> predicate, int? count = null);
    }
}
