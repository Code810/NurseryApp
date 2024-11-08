﻿using NurseryApp.Application.Dtos.FeeDto;

namespace NurseryApp.Application.Interfaces
{
    public interface IFeeService
    {
        Task<FeeReturnDto> CreateFeeAndAssignToStudent(int? groupId, FeeCreateDto feeCreateDto);
        Task<int> Update(int? id, FeeUpdateDto feeUpdateDto);
        Task<IEnumerable<FeeReturnDto>> GetAll(DateTime? date, string? text);
        Task<IEnumerable<FeeReturnDto>> GetAll(DateTime date);
        Task<IEnumerable<FeeReturnDto>> GetAll(int studentId);
        Task<FeeReturnDto> Get(int? id);
        Task<int> Delete(int? id);
    }
}
