using NurseryApp.Application.Dtos.FeeDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IFeeService
    {
        Task<int> Create(FeeCreateDto feeCreateDto);
        Task<int> Update(int? id, FeeUpdateDto feeUpdateDto);
        Task<IEnumerable<FeeReturnDto>> GetAll();
        Task<IEnumerable<FeeReturnDto>> GetAll(DateTime date);
        Task<IEnumerable<FeeReturnDto>> GetAll(int studentId);
        Task<FeeReturnDto> Get(DateTime date, int? studentId);
    }
}
