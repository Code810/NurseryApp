using NurseryApp.Application.Dtos.AppUser;

namespace NurseryApp.Application.Interfaces
{
    public interface IAppUserService
    {
        Task<int> Update(string? id, AppUserUpdateDto appUserUpdateDto);
        Task<IEnumerable<AppUserReturnDto>> GetAll(string? text);
        Task<AppUserReturnDto> Get(string? id);
        Task<int> Delete(string? id);
        Task<int> ChangeStatus(string? id);
    }
}
