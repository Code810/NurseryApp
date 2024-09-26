using NurseryApp.Application.Dtos.SettingDto;

namespace NurseryApp.Application.Interfaces
{
    public interface ISettingService
    {
        Task<int> Create(SettingCreateDto settingCreateDto);
        Task<int> Update(int? id, SettingUpdateDto settingUpdateDto);
        Task<IEnumerable<SettingReturnDto>> GetAll();
        Task<SettingReturnDto> Get(int? id);
        Task<SettingReturnDto> Get(string? key);
    }
}
