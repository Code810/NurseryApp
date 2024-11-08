﻿using Microsoft.AspNetCore.Identity;
using NurseryApp.Application.Dtos.Auth;
using NurseryApp.Application.Exceptions;
using NurseryApp.Application.Helpers;
using NurseryApp.Application.Interfaces;
using NurseryApp.Core.Entities;

namespace NurseryApp.Application.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _emailService = emailService;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<int> ConfirmEmail(string email, string token)
        {

            AppUser appUser = await _userManager.FindByEmailAsync(email);

            if (appUser == null)
            {
                throw new CustomException(400, "User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(appUser, token);

            if (!result.Succeeded)
            {
                throw new CustomException(400, "Email confirmation failed. The token might be invalid or expired.");
            }

            return 1;
        }

        public async Task<AppUser> Register(RegisterDto registerDto)
        {
            AppUser user = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                throw new CustomException(400, string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, RolesEnum.member.ToString());

            //string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //string link = $"{scheme}://{host}/api/Auth/confirm-email?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

            //string body;
            //using (StreamReader streamReader = new StreamReader("wwwroot/emailTemplate/emailConfirm.html"))
            //{
            //    body = await streamReader.ReadToEndAsync();
            //}
            //body = body.Replace("{{link}}", link);
            //body = body.Replace("{{username}}", user.UserName);

            //_emailService.SendEmail(body, new List<string> { user.Email }, "Email Verification", "Verify your email");

            return user;
        }

        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(RolesEnum)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var existUser = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if (existUser == null)
            {
                existUser = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
                if (existUser == null) throw new CustomException(401, "User or Pasword wrong");
            }
            var result = await _userManager.CheckPasswordAsync(existUser, loginDto.Password);
            if (!result) throw new CustomException(401, "User or Pasword wrong");
            if (!existUser.EmailConfirmed) throw new CustomException(401, "Please confirm email");
            var roles = await _userManager.GetRolesAsync(existUser);
            return _tokenService.GetToken(existUser, roles);

        }


        public async Task<string> ForgetPassword(string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) throw new CustomException(400, "User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            return token;
        }

        public async Task<int> ResetPasswordAsync(string email, string token, ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.Password != resetPasswordDto.RePassword) throw new CustomException(400, "the password should be the same as the Repassword");
            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                throw new CustomException(400, "User not found.");
            }

            var resetResult = await _userManager.ResetPasswordAsync(appUser, token, resetPasswordDto.Password);
            if (!resetResult.Succeeded)
            {
                var errors = string.Join(", ", resetResult.Errors.Select(e => e.Description));
                throw new CustomException(400, $"Password reset failed: {errors}");
            }
            await _userManager.UpdateSecurityStampAsync(appUser);
            return 1;
        }
    }
}
