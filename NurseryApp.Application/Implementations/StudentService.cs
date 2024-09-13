using AutoMapper;
using NurseryApp.Application.Dtos.StudentDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

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

        public async Task<int> Create(StudentCreateDto studentCreateDto)
        {
            var existParent = await _unitOfWork.parentRepository
                .GetAsync(p => p.Id == studentCreateDto.ParentId && !p.IsDeleted);
            if (existParent == null) throw new CustomException(404, "Parent not found");
            var student = _mapper.Map<Student>(studentCreateDto);
            await _unitOfWork.studentRepository.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();

            return student.Id;
        }
        public async Task<StudentReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Student ID cannot be null");
            var student = await _unitOfWork.studentRepository.GetByWithIncludesAsync(
                s => s.Id == id && !s.IsDeleted,
                s => s.Group,
                s => s.Parent);

            if (student == null) throw new CustomException(404, "Student not found");

            return _mapper.Map<StudentReturnDto>(student);
        }



        // Delete a student by ID
        public async Task<int> Delete(int? id)
        {
            if (id == null)
                throw new CustomException(400, "Student ID cannot be null");

            var student = await _unitOfWork.studentRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (student == null)
                throw new CustomException(404, "Student not found");

            student.IsDeleted = true; // Mark student as deleted (soft delete)
            _unitOfWork.studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return student.Id; // Return the deleted student's ID
        }



        // Get all students
        public async Task<IEnumerable<StudentReturnDto>> GetAll()
        {
            var students = await _unitOfWork.studentRepository.FindWithIncludesAsync(
                s => !s.IsDeleted,
                s => s.Group, // Assuming students have groups
                s => s.Parent); // Assuming students have parents

            return _mapper.Map<IEnumerable<StudentReturnDto>>(students);
        }

        // Update a student by ID
        public async Task<int> Update(int? id, StudentUpdateDto studentUpdateDto)
        {
            if (id == null)
                throw new CustomException(400, "Student ID cannot be null");

            var student = await _unitOfWork.studentRepository.GetAsync(s => s.Id == id && !s.IsDeleted);
            if (student == null)
                throw new CustomException(404, "Student not found");

            _mapper.Map(studentUpdateDto, student); // Map the updated DTO to the existing student entity
            _unitOfWork.studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return student.Id; // Return the updated student's ID
        }
    }

}
