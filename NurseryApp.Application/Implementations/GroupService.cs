﻿using AutoMapper;
using NurseryApp.Application.Dtos.GroupDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Create(GroupCreateDto groupCreateDto)
        {
            var existGroup = await _unitOfWork.groupRepository.IsExist(g => g.Name.ToLower() == groupCreateDto.Name.ToLower());
            if (existGroup) throw new CustomException(404, "That group name is taken. Try another");
            var existTeacher = await _unitOfWork.teacherRepository.GetByWithIncludesAsync(t => t.Id == groupCreateDto.TeacherId, t => t.Group);
            if (existTeacher == null) throw new CustomException(404, "Teacher not found");
            if (existTeacher.Group != null) throw new CustomException(404, "Teacher is busy select another teacher");
            var group = _mapper.Map<Group>(groupCreateDto);
            await _unitOfWork.groupRepository.AddAsync(group);
            await _unitOfWork.SaveChangesAsync();
            return group.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null)
                throw new CustomException(400, "Group ID not provided");

            var existGroup = await _unitOfWork.groupRepository.GetByWithIncludesAsync(
                g => g.Id == id.Value,
                g => g.Students,
                g => g.Teacher
            );

            if (existGroup == null)
                throw new CustomException(404, "Group not found");

            if (existGroup.Students.Any() || existGroup.Teacher != null)
                throw new CustomException(400, "Cannot delete group with associated students or a teacher");

            _unitOfWork.groupRepository.Remove(existGroup);

            await _unitOfWork.SaveChangesAsync();
            return existGroup.Id;
        }


        public async Task<GroupReturnDto> Get(string? name)
        {
            if (name == null) throw new CustomException(400, "group name not sent");
            var existGroup = await _unitOfWork.groupRepository.GetByWithIncludesAsync(g => g.Name.ToLower() == name.ToLower(), g => g.Teacher);
            if (existGroup == null) throw new CustomException(404, "Not Found");
            return _mapper.Map<GroupReturnDto>(existGroup);
        }

        public async Task<GroupReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "group id not sent");
            var existGroup = await _unitOfWork.groupRepository.GetByWithIncludesAsync(g => g.Id == id, g => g.Teacher);
            if (existGroup == null) throw new CustomException(404, "Not Found");
            return _mapper.Map<GroupReturnDto>(existGroup);
        }

        public async Task<IEnumerable<GroupReturnDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<GroupReturnDto>>(await _unitOfWork.groupRepository.GetAllWithIncludesAsync(g => g.Teacher));
        }

        public async Task<int> Update(int? id, GroupUpdateDto groupUpdateDto)
        {
            if (id == null) throw new CustomException(400, "group is not selected");
            var existGroup = await _unitOfWork.groupRepository.GetByWithIncludesAsync(g => g.Id == id, g => g.Teacher);
            if (existGroup == null) throw new CustomException(404, "Not found");
            var existTeacher = await _unitOfWork.teacherRepository.GetByWithIncludesAsync(t => t.Id == groupUpdateDto.TeacherId, t => t.Group);
            if (existTeacher == null) throw new CustomException(400, "Teacher not found");
            if (existTeacher.Group.Id != id) throw new CustomException(400, "Teacher is busy select another teacher");
            var existByNameGroup = await _unitOfWork.groupRepository.GetAsync(g => g.Name.ToLower() == groupUpdateDto.Name.ToLower() && g.Id != id);
            if (existByNameGroup != null) throw new CustomException(400, "This group name is already used please write another name");
            _mapper.Map(groupUpdateDto, existGroup);
            _unitOfWork.groupRepository.Update(existGroup);
            await _unitOfWork.SaveChangesAsync();
            return existGroup.Id;
        }
    }
}
