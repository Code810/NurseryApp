using NurseryApp.Application.Dtos.TeacherDto;

namespace NurseryApp.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<int> Create(TeacherCreateDto teacherCreateDto);
        Task<int> Update(int? id, TeacherUpdateDto teacherUpdateDto);
        Task<IEnumerable<TeacherReturnDto>> GetAll(int? count);
        Task<TeacherReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
