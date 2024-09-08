using NurseryApp.Application.Dtos.AttenDanceDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IAttenDanceService
    {
        Task<int> CreateOrUpdate(AttenDanceCreateDto attenDanceCreateDto);
        Task<IEnumerable<AttenDanceReturnDto>> GetAll();
        Task<IEnumerable<AttenDanceReturnDto>> GetAll(DateTime date);
        Task<IEnumerable<AttenDanceReturnDto>> GetAll(int studentId);
        Task<AttenDanceReturnDto> Get(DateTime date, int? studentId);
    }
}
