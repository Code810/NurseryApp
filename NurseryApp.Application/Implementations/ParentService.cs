using NurseryApp.Application.Dtos.ParentDto;
using NurseryApp.Application.Interfaces;

namespace NurseryApp.Application.Implementations
{
    public class ParentService : IParentService
    {
        public Task<int> Create(ParentCreateDto parentCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ParentReturnDto> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParentReturnDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(int? id, ParentUpdateDto parentUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
