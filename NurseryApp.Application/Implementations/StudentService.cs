using AutoMapper;
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
            Expression<Func<Student, bool>> predicate = parentId == null
                ? s => !s.IsDeleted
                : s => !s.IsDeleted && s.ParentId == parentId;

            var students = await _unitOfWork.studentRepository.FindWithIncludesAsync(
                predicate,
                s => s.Group,
                s => s.Parent,
                s => s.Fees);
            if (students.Count() <= 0) throw new CustomException(404, "Student empty list");

            return _mapper.Map<IEnumerable<StudentReturnDto>>(students);
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
