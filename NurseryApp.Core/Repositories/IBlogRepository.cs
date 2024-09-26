using NurseryApp.Core.Entities;
using System.Linq.Expressions;

namespace NurseryApp.Core.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetAllAsyncWithSorting(Expression<Func<Blog, bool>> predicate, int? count = null);
        Task<IEnumerable<Blog>> GetAllWithSearch(string? text, int page = 1);
        Task<int> GetAllCount(string? text);
    }
}
