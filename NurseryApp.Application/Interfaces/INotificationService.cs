using NurseryApp.Application.Dtos.HomeWorkSubmission;
using NurseryApp.Application.Dtos.NotificationDto;

namespace NurseryApp.Application.Interfaces
{
    public interface INotificationService
    {
        Task<int> Create(NotificationCreateDto notificationCreateDto);
        Task<int> Update(int? id, NotificationUpdateDto notificationUpdateDto);
        Task<IEnumerable<NotificationReturnDto>> GetAll();
        Task<IEnumerable<NotificationReturnDto>> GetAll(int? parentId);
        Task<HomeWorkSubmissionReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
