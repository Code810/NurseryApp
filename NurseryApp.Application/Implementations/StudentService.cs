using AutoMapper;
using NurseryApp.Application.Dtos.AttenDanceDto;
using NurseryApp.Application.Dtos.HomeWorkSubmission;
using NurseryApp.Application.Dtos.StudentDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Helpers;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;
using System.Linq.Expressions;

namespace NurseryApp.Application.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StudentReturnDto> Create(StudentCreateDto studentCreateDto)
        {
            var existParent = await _unitOfWork.parentRepository
                .GetAsync(p => p.Id == studentCreateDto.ParentId && !p.IsDeleted);
            if (existParent == null) throw new CustomException(404, "Parent not found");
            var student = _mapper.Map<Student>(studentCreateDto);
            await _unitOfWork.studentRepository.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();
            var studentDto = _mapper.Map<StudentReturnDto>(student);

            return studentDto;
        }
        public async Task<StudentReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Student ID cannot be null");
            var student = await _unitOfWork.studentRepository.GetByWithIncludesAsync(
                s => s.Id == id && !s.IsDeleted,
                s => s.Group,
                s => s.Parent,
                s => s.Fees);

            if (student == null) throw new CustomException(404, "Student not found");

            return _mapper.Map<StudentReturnDto>(student);
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Student ID cannot be null");
            var student = await _unitOfWork.studentRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (student == null) throw new CustomException(404, "Student not found");
            if (student.GroupId != null) throw new CustomException(404, "You can not delete Student in any group");
            student.IsDeleted = true;
            Helper.DeleteImage("student", student.FileName);
            _unitOfWork.studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();
            return student.Id;
        }

        public async Task<IEnumerable<StudentReturnDto>> GetAll(int? parentId)
        {
            Expression<Func<Student, bool>> predicate = parentId != null
                ? s => !s.IsDeleted && s.ParentId == parentId
                : s => !s.IsDeleted;

            var students = await _unitOfWork.studentRepository.FindWithIncludesAsync(
                predicate,
                s => s.Group,
                s => s.Parent,
                s => s.Fees);
            if (students.Count() <= 0) throw new CustomException(404, "Student empty list");

            return _mapper.Map<IEnumerable<StudentReturnDto>>(students);
        }

        public async Task<IEnumerable<StudentReturnDto>> GetAll(int? groupId, DateTime? date)
        {
            if (groupId == null) throw new CustomException(400, "Group Id not sent");
            if (date == null) throw new CustomException(400, "Date not sent");

            var students = await _unitOfWork.studentRepository.FindWithIncludesAsync(
                s => s.GroupId == groupId && !s.IsDeleted,
                s => s.Group,
                s => s.AttenDances);

            if (!students.Any()) throw new CustomException(404, "No students found");

            var studentDtos = _mapper.Map<IEnumerable<StudentReturnDto>>(students).ToList();

            foreach (var studentDto in studentDtos)
            {
                var attendance = students.FirstOrDefault(s => s.Id == studentDto.Id)?
                    .AttenDances?.FirstOrDefault(a => a.CreatedDate.Date == date.Value.Date);

                studentDto.AttenDance = _mapper.Map<AttenDanceReturnDto>(attendance);
            }

            return studentDtos;
        }

        public async Task<IEnumerable<StudentSubmissionReturnDto>> GetAll(int? groupId, int? homeWorkId)
        {
            if (groupId == null) throw new CustomException(400, "Group Id not sent");
            if (homeWorkId == null) throw new CustomException(400, "homework not selected");
            var homeWork = await _unitOfWork.homeWorkRepository.GetAsync(h => h.Id == homeWorkId);
            if (homeWork == null) throw new CustomException(400, "homework not found");

            var students = await _unitOfWork.studentRepository.FindWithIncludesAsync(
                s => s.GroupId == groupId && !s.IsDeleted,
                s => s.Group,
                s => s.AttenDances,
                s => s.HomeWorkSubmissions);

            if (!students.Any()) throw new CustomException(404, "No students found");

            var studentDtos = _mapper.Map<IEnumerable<StudentSubmissionReturnDto>>(students).ToList();

            foreach (var studentDto in studentDtos)
            {
                var attendance = students.FirstOrDefault(s => s.Id == studentDto.Id)?
                    .AttenDances?.FirstOrDefault(a => a.CreatedDate.Date == homeWork.CreatedDate.Date);
                var homeWorkSubmission = students.FirstOrDefault(s => s.Id == studentDto.Id)?
                    .HomeWorkSubmissions?.FirstOrDefault(a => a.HomeWorkId == homeWorkId);

                studentDto.AttenDance = _mapper.Map<AttenDanceReturnDto>(attendance);
                studentDto.HomeWorkSubmission = _mapper.Map<HomeWorkSubmissionReturnDto>(homeWorkSubmission);
            }

            return studentDtos;
        }



        public async Task<StudentReturnDto> Update(int? id, StudentUpdateDto studentUpdateDto)
        {
            if (id == null) throw new CustomException(400, "Student ID cannot be null");
            var student = await _unitOfWork.studentRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (student == null) throw new CustomException(404, "Student not found");
            if (studentUpdateDto.File != null)
            {
                Helper.DeleteImage("student", student.FileName);
            }
            _mapper.Map(studentUpdateDto, student);

            _unitOfWork.studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StudentReturnDto>(student);


        }
    }

}
