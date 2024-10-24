using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatMessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllAsync(string? senderAppUserId, string? ReceiverAppUserId)
        {
            if (senderAppUserId == null || ReceiverAppUserId == null) throw new CustomException(404, "id not send");
            var messages = await _unitOfWork.chatMessageRepository
                .FindAllAsync(m => (m.SenderAppUserId == senderAppUserId && m.ReceiverAppUserId == ReceiverAppUserId)
                || m.SenderAppUserId == ReceiverAppUserId && m.ReceiverAppUserId == senderAppUserId);
            messages = messages.OrderBy(m => m.CreatedDate);
            return messages;
        }

        public async Task<ChatMessage> GetByIdAsync(int id)
        {
            return await _unitOfWork.chatMessageRepository.GetByIdAsync(id);
        }

        public async Task AddChatMessageAsync(string senderAppUserId, string ReceiverAppUserId, string message)
        {
            var chatMessage = new ChatMessage
            {
                SenderAppUserId = senderAppUserId,
                ReceiverAppUserId = ReceiverAppUserId,
                Message = message,
                SentAt = DateTime.Now
            };

            await _unitOfWork.chatMessageRepository.AddAsync(chatMessage);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
