using NurseryApp.Application.Dtos.HomeWorkSubmission;
using NurseryApp.Application.Dtos.NotificationDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Application.Implementations
{
    public class NotificationService : INotificationService
    {
        public Task<int> Create(NotificationCreateDto notificationCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<HomeWorkSubmissionReturnDto> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotificationReturnDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotificationReturnDto>> GetAll(int? parentId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(int? id, NotificationUpdateDto notificationUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
