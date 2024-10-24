using NurseryApp.Core.Entities;

namespace NurseryApp.Application.Interfaces
{
    public interface IChatMessageService
    {
        Task<IEnumerable<ChatMessage>> GetAllAsync(string? senderAppUserId, string? ReceiverAppUserId);
        Task<ChatMessage> GetByIdAsync(int id);
        Task AddChatMessageAsync(string senderAppUserId, string ReceiverAppUserId, string message);
    }
}
