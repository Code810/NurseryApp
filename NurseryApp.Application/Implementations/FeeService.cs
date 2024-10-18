using AutoMapper;
using NurseryApp.Application.Dtos.FeeDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class FeeService : IFeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FeeReturnDto> CreateFeeAndAssignToStudent(int? groupId, FeeCreateDto feeCreateDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existStudent = await _unitOfWork.studentRepository.GetByWithIncludesAsync(
                    s => s.Id == feeCreateDto.StudentId && !s.IsDeleted, s => s.Group);

                if (existStudent == null) throw new CustomException(404, "Student Not Found");

                if (existStudent.Group == null && groupId != null)
                {

                    existStudent.GroupId = groupId.Value;
                    _unitOfWork.studentRepository.Update(existStudent);
                    await _unitOfWork.SaveChangesAsync();
                }

                var lastFee = await _unitOfWork.feeRepository.GetLaastFeeAsync(f => f.StudentId == feeCreateDto.StudentId && !f.IsDeleted);

                Fee fee = new Fee
                {
                    StudentId = feeCreateDto.StudentId,
                    PaidDate = DateTime.Now,
                    DueDate = lastFee != null ? lastFee.DueDate.AddMonths(1) : DateTime.Now.AddMonths(1),
                    Amount = feeCreateDto.Amount
                };

                await _unitOfWork.feeRepository.AddAsync(fee);

                await _unitOfWork.CommitTransactionAsync();

                var feeReturn = _mapper.Map<FeeReturnDto>(fee);

                return feeReturn;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new CustomException(500, $"An error occurred: {ex.Message}");
            }
        }


        public async Task<FeeReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(404, "Not Found");
            var fee = await _unitOfWork.feeRepository.GetByWithIncludesAsync(f => f.Id == id && !f.IsDeleted, f => f.Student);
            if (fee == null)
                throw new CustomException(404, "Not Found");
            var feeReturn = _mapper.Map<FeeReturnDto>(fee);
            return feeReturn;
        }

        public async Task<IEnumerable<FeeReturnDto>> GetAll(DateTime? date, string? text)
        {
            IEnumerable<Fee> fees;

            if (date != null && string.IsNullOrEmpty(text))
            {
                fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(
                    f => !f.IsDeleted && f.PaidDate.Date == date.Value.Date,
                    f => f.Student
                );
            }
            else if (date == null && !string.IsNullOrEmpty(text))
            {
                string loweredText = text.ToLower();
                fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(
                    f => !f.IsDeleted &&
                         (f.Student.FirstName.ToLower().Contains(loweredText) || f.Student.LastName.ToLower().Contains(loweredText)),
                    f => f.Student
                );
            }
            else if (date != null && !string.IsNullOrEmpty(text))
            {
                string loweredText = text.ToLower();
                fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(
                    f => !f.IsDeleted &&
                         f.PaidDate.Date == date.Value.Date &&
                         (f.Student.FirstName.ToLower().Contains(loweredText) || f.Student.LastName.ToLower().Contains(loweredText)),
                    f => f.Student
                );
            }
            else
            {
                fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(
                    f => !f.IsDeleted,
                    f => f.Student
                );
            }

            return _mapper.Map<IEnumerable<FeeReturnDto>>(fees);
        }


        public async Task<IEnumerable<FeeReturnDto>> GetAll(DateTime date)
        {
            if (date == null) throw new CustomException(400, "Select date");
            var fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(f => f.PaidDate.Date == date.Date && !f.IsDeleted, f => f.Student);
            if (fees.Count() == 0) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<FeeReturnDto>>(fees);
        }

        public async Task<IEnumerable<FeeReturnDto>> GetAll(int studentId)
        {
            if (studentId == null) throw new CustomException(400, "Not found");
            var fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(f => f.StudentId == studentId && !f.IsDeleted, f => f.Student);
            if (fees.Count() == 0) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<FeeReturnDto>>(fees);
        }

        public async Task<int> Update(int? id, FeeUpdateDto feeUpdateDto)
        {
            if (id is null) throw new CustomException(400, "Not found");
            var fee = await _unitOfWork.feeRepository.GetAsync(f => f.Id == id && !f.IsDeleted);
            if (fee == null) throw new CustomException(400, "Not found");
            fee.Amount = feeUpdateDto.Amount;
            fee.DueDate = feeUpdateDto.DueDate;
            _unitOfWork.feeRepository.Update(fee);
            await _unitOfWork.SaveChangesAsync();
            return fee.Id;

        }
        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Fee ID cannot be null");

            var fee = await _unitOfWork.feeRepository.GetAsync(f => f.Id == id && !f.IsDeleted);
            if (fee == null) throw new CustomException(404, "Fee not found");
            fee.IsDeleted = true;
            _unitOfWork.feeRepository.Update(fee);
            await _unitOfWork.SaveChangesAsync();
            return fee.Id;
        }

    }
}
