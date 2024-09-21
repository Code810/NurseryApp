using NurseryApp.Application.Dtos.GroupDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IGroupService
    {
        Task<int> Create(GroupCreateDto groupCreateDto);
        Task<int> Update(int? id, GroupUpdateDto groupUpdateDto);
        Task<int> Delete(int? id);
        Task<IEnumerable<GroupReturnDto>> GetAll(int? count);
        Task<GroupReturnDto> Get(string? name);
        Task<GroupReturnDto> Get(int? id);
    }
}
