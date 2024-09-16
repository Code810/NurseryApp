using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Application.Dtos.TeacherDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Helpers;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> Create(TeacherCreateDto teacherCreateDto)
        {
            var appUser = await _userManager.Users
        .Include(u => u.Parent)
        .Include(u => u.Teacher)
        .FirstOrDefaultAsync(u => u.Id == teacherCreateDto.AppUserId);
            if (appUser == null) throw new CustomException(400, "User not found");

            if (appUser.Parent != null || appUser.Teacher != null) throw new CustomException(400, "User is already associated with a parent or teacher");

            var teacher = _mapper.Map<Teacher>(teacherCreateDto);
            teacher.AppUserId = teacherCreateDto.AppUserId;

            await _unitOfWork.teacherRepository.AddAsync(teacher);
            await _unitOfWork.SaveChangesAsync();


            return teacher.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Teacherr ID cannot be null");
            var teacher = await _unitOfWork.teacherRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (teacher == null) throw new CustomException(404, "Teacher not found");
            teacher.IsDeleted = true;
            var group = await _unitOfWork.groupRepository.GetAsync(t => t.TeacherId == id);
            if (group != null)
            {
                group.TeacherId = null;
                _unitOfWork.groupRepository.Update(group);
            }
            Helper.DeleteImage("teacher", teacher.FileName);
            _unitOfWork.teacherRepository.Update(teacher);
            await _unitOfWork.SaveChangesAsync();
            return teacher.Id;
        }

        public async Task<TeacherReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Teacher ID cannot be null");
            var teacher = await _unitOfWork.teacherRepository.GetByWithIncludesAsync(
                t => t.Id == id && !t.IsDeleted,
                t => t.Group,
                t => t.AppUser);

            if (teacher == null) throw new CustomException(404, "Teacher not found");
            var teachertDto = _mapper.Map<TeacherReturnDto>(teacher);
            return teachertDto;
        }

        public async Task<IEnumerable<TeacherReturnDto>> GetAll()
        {
            var teachers = await _unitOfWork.teacherRepository.FindWithIncludesAsync(
              t => !t.IsDeleted,
              t => t.Group,
              t => t.AppUser);
            if (teachers.Count() <= 0) throw new CustomException(404, "Empty teacher List");
            var teachersDtos = _mapper.Map<IEnumerable<TeacherReturnDto>>(teachers);

            return teachersDtos;
        }

        public async Task<int> Update(int? id, TeacherUpdateDto teacherUpdateDto)
        {
            if (id == null) throw new CustomException(400, "Teacher ID cannot be null");
            var teacher = await _unitOfWork.teacherRepository.GetAsync(t => t.Id == id && !t.IsDeleted);
            if (teacher == null) throw new CustomException(404, "Teacher not found");
            if (teacherUpdateDto.File != null)
            {
                Helper.DeleteImage("teacher", teacher.FileName);
            }
            _mapper.Map(teacherUpdateDto, teacher);
            _unitOfWork.teacherRepository.Update(teacher);
            await _unitOfWork.SaveChangesAsync();
            return teacher.Id;
        }
    }
}
