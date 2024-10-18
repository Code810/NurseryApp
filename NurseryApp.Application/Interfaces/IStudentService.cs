using NurseryApp.Application.Dtos.StudentDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentReturnDto> Create(StudentCreateDto studentCreateDto);
        Task<StudentReturnDto> Update(int? id, StudentUpdateDto studentUpdateDto);
        Task<IEnumerable<StudentReturnDto>> GetAll(int? parentId);
        Task<IEnumerable<StudentReturnDto>> GetAll(int? groupId, DateTime? date);
        Task<IEnumerable<StudentSubmissionReturnDto>> GetAll(int? groupId, int? homeWorkId);
        Task<StudentReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
