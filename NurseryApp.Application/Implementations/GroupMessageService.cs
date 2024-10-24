using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupMessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GroupMessage>> GetAllAsync()
        {
            return await _unitOfWork.groupMessageRepository.GetAllAsync();
        }

        public async Task<GroupMessage> GetByIdAsync(int id)
        {
            return await _unitOfWork.groupMessageRepository.GetByIdAsync(id);
        }

        public async Task AddGroupMessageAsync(int groupId, string message)
        {
            var groupMessage = new GroupMessage
            {
                GroupId = groupId,
                Message = message,
                SentAt = DateTime.Now
            };

            await _unitOfWork.groupMessageRepository.AddAsync(groupMessage);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
