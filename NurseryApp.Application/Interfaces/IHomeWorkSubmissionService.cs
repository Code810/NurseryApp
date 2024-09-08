using NurseryApp.Application.Dtos.HomeWorkSubmission;

namespace NurseryApp.Application.Interfaces
{
    public interface IHomeWorkSubmissionService
    {
        Task<int> Create(HomeWorkSubmissionCreateDto homeWorkSubmissionCreate);
        Task<int> Update(int? id, HomeWorkSubmissionUpdateDto homeWorkSubmissionUpdate);
        Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll();
        Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll(int? homeWorkId);
        Task<HomeWorkSubmissionReturnDto> Get(int? id);

    }
}
