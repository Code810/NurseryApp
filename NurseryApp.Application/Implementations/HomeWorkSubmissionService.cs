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
            var existHomeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.IsExist(h => h.CreatedDate.Date == DateTime.Now.Date && h.StudentId == homeWorkSubmissionCreate.StudentId);
            if (existHomeWorkSubmission) throw new CustomException(400, "Today homeworkSubmission for this student is already created");
            var existStudent = await _unitOfWork.studentRepository.IsExist(s => s.Id == homeWorkSubmissionCreate.StudentId);
            if (!existStudent) throw new CustomException(400, "Selected student not found");
            var homeWorkSubmission = _mapper.Map<HomeWorkSubmission>(homeWorkSubmissionCreate);
            homeWorkSubmission.CreatedDate = DateTime.Now;
            await _unitOfWork.homeWorkSubmissionRepository.AddAsync(homeWorkSubmission);
            await _unitOfWork.SaveChangesAsync();
            return homeWorkSubmission.Id;
        }

        public async Task<HomeWorkSubmissionReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "HomeworkSubmission not selected");
            var homeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.GetByWithIncludesAsync(h => h.Id == id, h => h.Student, h => h.HomeWork);
            if (homeWorkSubmission == null) throw new CustomException(400, "HomeworkSubmission not found");
            return _mapper.Map<HomeWorkSubmissionReturnDto>(homeWorkSubmission);
        }

        public async Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll()
        {
            var homeWorkSubmissions = await _unitOfWork.homeWorkSubmissionRepository.GetAllWithIncludesAsync(h => h.Student, h => h.HomeWork);
            if (homeWorkSubmissions == null) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<HomeWorkSubmissionReturnDto>>(homeWorkSubmissions);
        }

        public async Task<IEnumerable<HomeWorkSubmissionReturnDto>> GetAll(int? homeWorkId)
        {
            if (homeWorkId == null) throw new CustomException(400, "homework not selected");
            var homeWorkSubmissions = await _unitOfWork.homeWorkSubmissionRepository.FindWithIncludesAsync(h => h.HomeWorkId == homeWorkId, h => h.Student, h => h.HomeWork);
            if (homeWorkSubmissions == null) throw new CustomException(400, "Empty List");
            return _mapper.Map<IEnumerable<HomeWorkSubmissionReturnDto>>(homeWorkSubmissions);
        }

        public async Task<int> Update(int? id, HomeWorkSubmissionUpdateDto homeWorkSubmissionUpdate)
        {
            if (id == null) throw new CustomException(400, "homework not selected");
            var existHomeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.GetAsync(h => h.Id == id);
            if (existHomeWorkSubmission == null) throw new CustomException(400, "not found");
            var homeWorkSubmission = await _unitOfWork.homeWorkSubmissionRepository.GetAsync(h => h.StudentId == homeWorkSubmissionUpdate.StudentId &&
            h.HomeWorkId == homeWorkSubmissionUpdate.HomeWorkId && //birde fikirlesmek lazimdi mentiqi
            h.SubmissionDate.Date == existHomeWorkSubmission.SubmissionDate.Date);
            var updatedHomeWorkSubmission = _mapper.Map(homeWorkSubmissionUpdate, existHomeWorkSubmission);
            _unitOfWork.homeWorkSubmissionRepository.Update(updatedHomeWorkSubmission);
            _unitOfWork.SaveChangesAsync();
            return updatedHomeWorkSubmission.Id;
        }
    }
}
