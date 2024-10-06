using NurseryApp.Application.Dtos.StudentDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentReturnDto> Create(StudentCreateDto studentCreateDto);
        Task<StudentReturnDto> Update(int? id, StudentUpdateDto studentUpdateDto);
        Task<IEnumerable<StudentReturnDto>> GetAll(int? parentId);
        Task<StudentReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
