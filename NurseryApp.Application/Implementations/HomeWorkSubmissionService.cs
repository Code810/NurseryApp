using AutoMapper;
using NurseryApp.Application.Dtos.HomeWorkSubmission;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class HomeWorkSubmissionService : IHomeWorkSubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeWorkSubmissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Create(HomeWorkSubmissionCreateDto homeWorkSubmissionCreate)
        {
            var existHomeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.IsExist(h => h.CreatedDate.Date == DateTime.Now.Date && h.StudentId == homeWorkSubmissionCreate.StudentId && !h.IsDeleted);
            if (existHomeWorkSubmission) throw new CustomException(400, "Today homeworkSubmission for this student is already created");
            var existStudent = await _unitOfWork.studentRepository.IsExist(s => s.Id == homeWorkSubmissionCreate.StudentId && !s.IsDeleted);
            if (!existStudent) throw new CustomException(400, "Selected student not found");
            var homeWorkSubmission = _mapper.Map<HomeWorkSubmission>(homeWorkSubmissionCreate);
            homeWorkSubmission.CreatedDate = DateTime.Now;
            await _unitOfWork.homeWorkSubmissionRepository.AddAsync(homeWorkSubmission);
            await _unitOfWork.SaveChangesAsync();
            return homeWorkSubmission.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "HomeworkSubmission ID not provided");
            var existHomeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.GetAsync(h => h.Id == id && !h.IsDeleted);

            if (existHomeWorkSubmission == null) throw new CustomException(404, "HomeworkSubmission not found");

            existHomeWorkSubmission.IsDeleted = true;
            _unitOfWork.homeWorkSubmissionRepository.Update(existHomeWorkSubmission);
            await _unitOfWork.SaveChangesAsync();
            return existHomeWorkSubmission.Id;
        }

        public async Task<HomeWorkSubmissionReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "HomeworkSubmission not selected");
            var homeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.GetByWithIncludesAsync(h => h.Id == id && !h.IsDeleted, h => h.Student, h => h.HomeWork);
            if (homeWorkSubmission == null) throw new CustomException(400, "HomeworkSubmission not found");
            return _mapper.Map<HomeWorkSubmissionReturnDto>(homeWorkSubmission);
        }

        public async Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll()
        {
            var homeWorkSubmissions = await _unitOfWork.homeWorkSubmissionRepository.FindWithIncludesAsync(h => !h.IsDeleted, h => h.Student, h => h.HomeWork);
            if (homeWorkSubmissions == null) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<HomeWorkSubmissionReturnDto>>(homeWorkSubmissions);
        }

        public async Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll(int? homeWorkId)
        {
            if (homeWorkId == null) throw new CustomException(400, "homework not selected");
            var homeWorkSubmissions = await _unitOfWork.homeWorkSubmissionRepository.FindWithIncludesAsync(h => h.HomeWorkId == homeWorkId && !h.IsDeleted, h => h.Student, h => h.HomeWork);
            if (homeWorkSubmissions.Count() <= 0) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<HomeWorkSubmissionReturnDto>>(homeWorkSubmissions);
        }

        public async Task<int> Update(int? id, HomeWorkSubmissionUpdateDto homeWorkSubmissionUpdate)
        {
            if (id == null) throw new CustomException(400, "homework not selected");
            var existHomeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.GetAsync(h => h.Id == id && !h.IsDeleted);
            if (existHomeWorkSubmission == null) throw new CustomException(400, "not found");
            var updatedHomeWorkSubmission = _mapper.Map(homeWorkSubmissionUpdate, existHomeWorkSubmission);
            _unitOfWork.homeWorkSubmissionRepository.Update(updatedHomeWorkSubmission);
            await _unitOfWork.SaveChangesAsync();
            return updatedHomeWorkSubmission.Id;
        }
    }
}
