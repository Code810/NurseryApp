using NurseryApp.Application.Dtos.HomeWork;

namespace NurseryApp.Application.Interfaces
{
    public interface IHomeWorkService
    {
        Task<int> Create(HomeWorkCreateDto homeWorkCreateDto);
        Task<int> Update(int? id, HomeWorkUpdateDto homeWorkUpdateDto);
        Task<IEnumerable<HomeWorkReturnDto>> GetAll();
        Task<IEnumerable<HomeWorkReturnDto>> GetAll(int? groupId);
        Task<HomeWorkReturnDto> Get(int? id);

    }
}
