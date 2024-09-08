using NurseryApp.Application.Dtos.TeacherDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Application.Implementations
{
    public class TeacherService : ITeacherService
    {
        public Task<int> Create(TeacherCreateDto teacherCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<TeacherReturnDto> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeacherReturnDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(int? id, TeacherUpdateDto teacherUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
