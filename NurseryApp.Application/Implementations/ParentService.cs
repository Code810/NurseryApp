using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Application.Dtos.ParentDto;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Helpers;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ParentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> Create(ParentCreateDto parentCreateDto)
        {
            var appUser = await _userManager.Users
        .Include(u => u.Parent)
        .Include(u => u.Teacher)
        .FirstOrDefaultAsync(u => u.Id == parentCreateDto.AppUserId);
            if (appUser == null) throw new CustomException(400, "User not found");

            if (appUser.Parent != null || appUser.Teacher != null) throw new CustomException(400, "User is already associated with a parent");

            var parent = _mapper.Map<Parent>(parentCreateDto);
            parent.AppUserId = parentCreateDto.AppUserId;
            await _userManager.AddToRoleAsync(appUser, RolesEnum.parent.ToString());

            try
            {
                await _unitOfWork.parentRepository.AddAsync(parent);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }


            return parent.Id;
        }

        public async Task<int> Delete(int? id)
        {
            if (id == null) throw new CustomException(400, "Parent ID cannot be null");
            var parent = await _unitOfWork.parentRepository.GetAsync(p => p.Id == id && !p.IsDeleted);
            if (parent == null) throw new CustomException(404, "Parent not found");
            parent.IsDeleted = true;
            _unitOfWork.parentRepository.Update(parent);
            await _unitOfWork.SaveChangesAsync();
            return parent.Id;
        }

        public async Task<ParentReturnDto> Get(int? id)
        {
            if (id == null) throw new CustomException(400, "Parent ID cannot be null");
            var parent = await _unitOfWork.parentRepository.GetByWithIncludesAsync(
                p => p.Id == id && !p.IsDeleted,
                p => p.Students,
                p => p.AppUser);

            if (parent == null) throw new CustomException(404, "Parent not found");
            var parentDto = _mapper.Map<ParentReturnDto>(parent);
            return parentDto;
        }
        public async Task<ParentReturnDto> GetByAppUserId(string? id)
        {
            if (id == null) throw new CustomException(400, "User ID cannot be null");
            var parent = await _unitOfWork.parentRepository.GetByAppUserId(id);

            if (parent == null) throw new CustomException(404, "Parent not found");
            var parentDto = _mapper.Map<ParentReturnDto>(parent);
            return parentDto;
        }

        public async Task<IEnumerable<ParentReturnDto>> GetAll()
        {
            var parents = await _unitOfWork.parentRepository.FindWithIncludesAsync(
                p => !p.IsDeleted,
                p => p.Students,
                p => p.AppUser);
            if (parents.Count() <= 0) throw new CustomException(404, "Empty Parent List");
            var parentDtos = _mapper.Map<IEnumerable<ParentReturnDto>>(parents);

            return parentDtos;
        }

        public async Task<int> Update(int? id, ParentUpdateDto parentUpdateDto)
        {
            if (id == null) new CustomException(404, "Parent not found");

            var parent = await _unitOfWork.parentRepository.GetAsync(p => p.Id == id && !p.IsDeleted);
            if (parent == null) throw new CustomException(404, "Parent not found");

            _mapper.Map(parentUpdateDto, parent);
            _unitOfWork.parentRepository.Update(parent);
            await _unitOfWork.SaveChangesAsync();

            return parent.Id;
        }
    }
}
