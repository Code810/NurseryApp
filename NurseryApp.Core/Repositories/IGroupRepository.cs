using NurseryApp.Core.Entities;
using System.Linq.Expressions;

namespace NurseryApp.Core.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<Group>> GetAllAsyncWithSorting(Expression<Func<Group, bool>> predicate, int? count = null, params Expression<Func<Group, object>>[] includes);
        Task<IEnumerable<Group>> GetAllWithSearch(string? text, int page = 1, params Expression<Func<Group, object>>[] includes);
        Task<int> GetAllCount(string? text);
        Task<Group> GetGroupById(int groupId);
    }
}
