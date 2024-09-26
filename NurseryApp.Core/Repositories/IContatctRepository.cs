using NurseryApp.Core.Entities;

namespace NurseryApp.Core.Repositories
{
    public interface IContatctRepository : IRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetAllWithSearch(string? text, int page = 1);
        Task<int> GetAllCount(string? text);
    }
}
