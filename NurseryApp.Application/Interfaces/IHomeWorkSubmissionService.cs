using NurseryApp.Application.Dtos.HomeWorkSubmission;

namespace NurseryApp.Application.Interfaces
{
    public interface IHomeWorkSubmissionService
    {
        Task<int> Create(HomeWorkSubmissionCreateDto homeWorkSubmissionCreate);
        Task<int> Update(int? id, HomeWorkSubmissionUpdateDto homeWorkSubmissionUpdate);
        Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll();
        Task<int> Delete(int? id);
        Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll(int? homeWorkId, int? studentId);
        Task<HomeWorkSubmissionReturnDto> Get(int? id);

    }
}
