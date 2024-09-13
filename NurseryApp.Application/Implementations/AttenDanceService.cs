using AutoMapper;
using NurseryApp.Application.Dtos.AttenDanceDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class AttenDanceService : IAttenDanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttenDanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateOrUpdate(AttenDanceCreateDto attenDanceCreateDto)
        {
            var attenDance = await _unitOfWork.attenDanceRepository.GetAsync(a => a.StudentId == attenDanceCreateDto.StudentId
            && a.Date.Date == DateTime.Now.Date && !a.IsDeleted);
            if (attenDance == null)
            {
                attenDance = _mapper.Map<AttenDance>(attenDanceCreateDto);
                attenDance.CreatedDate = DateTime.Now;
                attenDance.Date = DateTime.Now;
                await _unitOfWork.attenDanceRepository.AddAsync(attenDance);
            }
            else
            {
                attenDance.Status = attenDanceCreateDto.Status;
                attenDance.UpdatedDate = DateTime.Now;
                _unitOfWork.attenDanceRepository.Update(attenDance);
            }
            await _unitOfWork.SaveChangesAsync();
            return attenDance.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Attendance ID cannot be null");

            var attenDance = await _unitOfWork.attenDanceRepository.GetAsync(a => a.Id == id && !a.IsDeleted);
            if (attenDance == null) throw new CustomException(404, "Attendance not found");
            attenDance.IsDeleted = true;
            _unitOfWork.attenDanceRepository.Update(attenDance);
            await _unitOfWork.SaveChangesAsync();
            return attenDance.Id;
        }

        public async Task<AttenDanceReturnDto> Get(DateTime date, int? studentId)
        {
            if (studentId == null) throw new CustomException(404, "Not Found");
            var attenDance = await _unitOfWork.attenDanceRepository.GetByWithIncludesAsync(a => a.StudentId == studentId
            && a.Date.Date == date.Date && !a.IsDeleted, a => a.Student);
            if (attenDance == null)
                throw new CustomException(404, "Not Found");
            var attenDanceReturn = _mapper.Map<AttenDanceReturnDto>(attenDance);
            return attenDanceReturn;
        }

        public async Task<IEnumerable<AttenDanceReturnDto>> GetAll()
        {
            var attenDances = await _unitOfWork.attenDanceRepository.FindWithIncludesAsync(a => !a.IsDeleted, a => a.Student);
            return _mapper.Map<IEnumerable<AttenDanceReturnDto>>(attenDances);
        }

        public async Task<IEnumerable<AttenDanceReturnDto>> GetAll(DateTime date)
        {
            if (date == null) throw new CustomException(400, "Select date");
            var attenDances = await _unitOfWork.attenDanceRepository.FindWithIncludesAsync(a => a.Date.Date == date.Date && !a.IsDeleted, a => a.Student);
            if (attenDances.Count() == 0) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<AttenDanceReturnDto>>(attenDances); ;
        }

        public async Task<IEnumerable<AttenDanceReturnDto>> GetAll(int studentId)
        {
            if (studentId == null) throw new CustomException(400, "Not found");
            var attenDances = await _unitOfWork.attenDanceRepository.FindWithIncludesAsync(a => a.StudentId == studentId && !a.IsDeleted, a => a.Student);
            if (attenDances.Count() == 0) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<AttenDanceReturnDto>>(attenDances);
        }


    }
}
