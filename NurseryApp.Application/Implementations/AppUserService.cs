using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Application.Dtos.AppUser;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Application.Implementations
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AppUserService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> ChangeStatus(string? id)
        {
            if (id == null) throw new CustomException(400, "User Id not send");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new CustomException(400, "User not found");
            user.IsBlocked = !user.IsBlocked;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new CustomException(500, "Failed to update user status");
            return user.IsBlocked ? 1 : 0;
        }

        public async Task<int> Delete(string? id)
        {
            if (id == null) throw new CustomException(400, "User Id not send");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new CustomException(400, "User not found");
            user.IsDeleted = true;
            var exisTeacher = await _unitOfWork.teacherRepository.GetAsync(t => t.AppUserId == id);
            if (exisTeacher != null)
            {
                exisTeacher.IsDeleted = true;
                _unitOfWork.teacherRepository.Update(exisTeacher);
            }
            var existParent = await _unitOfWork.parentRepository.GetAsync(p => p.AppUserId == id);
            if (existParent != null)
            {
                existParent.IsDeleted = true;
                _unitOfWork.parentRepository.Update(existParent);
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) throw new CustomException(500, "Failed to update user status");
            await _unitOfWork.SaveChangesAsync();
            return user.IsBlocked ? 1 : 0;
        }

        public async Task<AppUserReturnDto> Get(string? id)
        {
            if (id == null) throw new CustomException(400, "User Id not send");
            var user = await _userManager.Users
                .Include(u => u.Teacher)
                .Include(u => u.Parent)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) throw new CustomException(400, "User not found");

            return _mapper.Map<AppUserReturnDto>(user);
        }

        public async Task<IEnumerable<AppUserReturnDto>> GetAll(string? text)
        {
            IEnumerable<AppUser> users;
            users = await _userManager.Users
                .Include(u => u.Teacher)
                .Include(u => u.Parent).Where(u => !u.IsDeleted).ToListAsync();
            if (text != null)
            {
                users = users.Where(u => u.UserName.ToLower().Contains(text.ToLower()) ||
                 u.FirstName.ToLower().Contains(text.ToLower()) || u.Email.ToLower().Contains(text.ToLower()) ||
                 u.LastName.ToLower().Contains(text.ToLower()));
            }
            if (users.Count() <= 0) throw new CustomException(400, "Empty User list");
            var userDtos = _mapper.Map<IEnumerable<AppUserReturnDto>>(users);

            // Fetch and assign roles for each user
            foreach (var user in userDtos)
            {
                var appUser = users.FirstOrDefault(u => u.Id == user.Id);
                user.Roles = (await _userManager.GetRolesAsync(appUser)).ToList();
            }

            return userDtos;
        }

        public async Task<IEnumerable<AppUserReturnDto>> GetAllByRole(string? text)
        {
            var query = _userManager.Users
                .Include(u => u.Teacher)
                .Include(u => u.Parent)
                .Where(u => !u.IsDeleted);

            if (!string.IsNullOrEmpty(text))
            {
                var lowerText = text.ToLower();
                query = query.Where(u => u.UserName.ToLower().Contains(lowerText) ||
                                         u.FirstName.ToLower().Contains(lowerText) ||
                                         u.LastName.ToLower().Contains(lowerText) ||
                                         u.Email.ToLower().Contains(lowerText));
            }

            var users = await query.ToListAsync();

            if (!users.Any())
            {
                throw new CustomException(400, "Empty User list");
            }

            var userDtos = _mapper.Map<IEnumerable<AppUserReturnDto>>(users);

            var result = new List<AppUserReturnDto>();

            foreach (var user in userDtos)
            {
                var appUser = users.FirstOrDefault(u => u.Id == user.Id);
                if (appUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(appUser);

                    if (roles.Contains("member") && !roles.Any(r => r == "teacher" || r == "parent" || r == "admin"))
                    {
                        user.Roles = roles.ToList();
                        result.Add(user);
                    }
                }
            }

            return result;
        }


        public async Task<int> Update(string? id, AppUserUpdateDto appUserUpdateDto)
        {
            if (id == null) throw new CustomException(400, "User Id not send");
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) throw new CustomException(400, "User not found");
            var existUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == appUserUpdateDto.UserName && u.Id != id);
            if (existUser != null) throw new CustomException(400, "Bu username artıq istifadə edilib");
            var existUserByEmaail = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == appUserUpdateDto.Email && u.Id != id);
            if (existUserByEmaail != null) throw new CustomException(400, "Bu email artıq istifadə edilib");
            if (!await _userManager.CheckPasswordAsync(user, appUserUpdateDto.Password))
            {
                throw new CustomException(400, "Şifrə yalnışdır");
            }

            _mapper.Map(appUserUpdateDto, user);

            if (appUserUpdateDto.NewPassword != null && appUserUpdateDto.NewRePassword != null)
            {

                var result = await _userManager.ChangePasswordAsync(user, appUserUpdateDto.Password, appUserUpdateDto.NewRePassword);

                if (!result.Succeeded) throw new CustomException(500, "Failed to update password");
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) throw new CustomException(500, "Failed to update user details");

            return 1;
        }

        public async Task<AppUserReturnDto> UpdateForAdmin(string? id, AppUserUpdateForAdminDto appUserUpdateDto)
        {
            if (id == null) throw new CustomException(400, "User Id not send");
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) throw new CustomException(400, "User not found");
            var existUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == appUserUpdateDto.UserName && u.Id != id);
            if (existUser != null) throw new CustomException(400, "Bu username artıq istifadə edilib");
            var existUserByEmaail = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == appUserUpdateDto.Email && u.Id != id);
            if (existUserByEmaail != null) throw new CustomException(400, "Bu email artıq istifadə edilib");

            _mapper.Map(appUserUpdateDto, user);

            if (!string.IsNullOrEmpty(appUserUpdateDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, appUserUpdateDto.Password);

                if (!result.Succeeded) throw new CustomException(500, "Failed to update password");
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) throw new CustomException(500, "Failed to update user details");

            return _mapper.Map<AppUserReturnDto>(user);
        }
    }


}
