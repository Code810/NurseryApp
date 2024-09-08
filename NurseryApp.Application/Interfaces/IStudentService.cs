using NurseryApp.Application.Dtos.StudentDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IStudentService
    {
        Task<int> Create(StudentCreateDto studentCreateDto);
        Task<int> Update(int? id, StudentUpdateDto studentUpdateDto);
        Task<IEnumerable<StudentReturnDto>> GetAll();
        Task<StudentReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
