using NurseryApp.Application.Dtos.ParentDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IParentService
    {
        Task<int> Create(ParentCreateDto parentCreateDto);
        Task<int> Update(int? id, ParentUpdateDto parentUpdateDto);
        Task<ParentListDto> GetAll(string? text, int page = 1);
        Task<ParentReturnDto> Get(int? id);
        Task<int> Delete(int? id);
        Task<ParentReturnDto> GetByAppUserId(string? id);
    }
}
