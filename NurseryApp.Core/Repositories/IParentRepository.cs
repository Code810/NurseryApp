using NurseryApp.Core.Entities;

namespace NurseryApp.Core.Repositories
{
    public interface IParentRepository : IRepository<Parent>
    {
        Task<Parent> GetByAppUserId(string id);
    }
}
