using NurseryApp.Application.Dtos.ParentDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IParentService
    {
        Task<int> Create(ParentCreateDto parentCreateDto);
        Task<int> Update(int? id, ParentUpdateDto parentUpdateDto);
        Task<IEnumerable<ParentReturnDto>> GetAll();
        Task<ParentReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
