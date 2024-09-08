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
        public async Task<int> Create(FeeCreateDto feeCreateDto)
        {
            var existStudent = await _unitOfWork.studentRepository.GetAsync(s => s.Id == feeCreateDto.StudentId);
            if (existStudent == null) throw new CustomException(404, "Not Found");
            var lastFee = await _unitOfWork.feeRepository.GetAsync(f => f.StudentId == feeCreateDto.StudentId && f.DueDate > DateTime.Now.AddMonths(-1));
            Fee fee = new();
            fee.StudentId = feeCreateDto.StudentId;
            fee.PaidDate = DateTime.Now;
            fee.DueDate = lastFee != null ? lastFee.DueDate.AddMonths(1) : DateTime.Now.AddMonths(1);
            fee.Amount = feeCreateDto.Amount;
            await _unitOfWork.feeRepository.AddAsync(fee);
            await _unitOfWork.SaveChangesAsync();
            return fee.StudentId;
        }

        public async Task<FeeReturnDto> Get(DateTime date, int? studentId)
        {
            if (studentId == null) throw new CustomException(404, "Not Found");
            var fee = await _unitOfWork.feeRepository.GetByWithIncludesAsync(f => f.StudentId == studentId
            && f.PaidDate.Date == date.Date, f => f.Student);
            if (fee == null)
                throw new CustomException(404, "Not Found");
            var feeReturn = _mapper.Map<FeeReturnDto>(fee);
            return feeReturn;
        }

        public async Task<IEnumerable<FeeReturnDto>> GetAll()
        {
            var fees = await _unitOfWork.feeRepository.GetAllWithIncludesAsync(f => f.Student);
            return _mapper.Map<IEnumerable<FeeReturnDto>>(fees);
        }

        public async Task<IEnumerable<FeeReturnDto>> GetAll(DateTime date)
        {
            if (date == null) throw new CustomException(400, "Select date");
            var fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(f => f.PaidDate.Date == date.Date, f => f.Student);
            if (fees.Count() == 0) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<FeeReturnDto>>(fees);
        }

        public async Task<IEnumerable<FeeReturnDto>> GetAll(int studentId)
        {
            if (studentId == null) throw new CustomException(400, "Not found");
            var fees = await _unitOfWork.feeRepository.FindWithIncludesAsync(f => f.StudentId == studentId, f => f.Student);
            if (fees.Count() == 0) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<FeeReturnDto>>(fees);
        }

        public async Task<int> Update(int? id, FeeUpdateDto feeUpdateDto)
        {
            if (id is null) throw new CustomException(400, "Not found");
            var fee = await _unitOfWork.feeRepository.GetAsync(f => f.Id == id);
            if (fee == null) throw new CustomException(400, "Not found");
            fee.Amount = feeUpdateDto.Amount;
            fee.DueDate = feeUpdateDto.DueDate;
            _unitOfWork.feeRepository.Update(fee);
            _unitOfWork.SaveChangesAsync();
            return fee.Id;

        }
    }
}
