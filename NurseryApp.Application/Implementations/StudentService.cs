using NurseryApp.Application.Dtos.StudentDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Application.Implementations
{
    public class StudentService : IStudentService
    {
        public Task<int> Create(StudentCreateDto studentCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentReturnDto> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentReturnDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(int? id, StudentUpdateDto studentUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
