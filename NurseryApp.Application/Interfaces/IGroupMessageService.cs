using NurseryApp.Core.Entities;

namespace NurseryApp.Application.Interfaces
{
    public interface IGroupMessageService
    {
        Task<IEnumerable<GroupMessage>> GetAllAsync();
        Task<GroupMessage> GetByIdAsync(int id);
        Task AddGroupMessageAsync(int groupId, string message);
    }
}
