using NurseryApp.Core.Entities;
using System.Linq.Expressions;

namespace NurseryApp.Core.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAllAsyncWithSorting(Expression<Func<Teacher, bool>> predicate, int? count = null, params Expression<Func<Teacher, object>>[] includes);
    }
}
