using Microsoft.AspNetCore.Identity;
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

        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IEmailService emailService, ITokenService tokenService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _emailService = emailService;
            _tokenService = tokenService;
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

        public async Task<int> Register(RegisterDto registerDto, string scheme, string host)
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

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string link = $"{scheme}://{host}/api/Auth/confirm-email?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";

            string body;
            using (StreamReader streamReader = new StreamReader("wwwroot/emailTemplate/emailConfirm.html"))
            {
                body = await streamReader.ReadToEndAsync();
            }
            body = body.Replace("{{link}}", link);
            body = body.Replace("{{username}}", user.UserName);

            _emailService.SendEmail(body, new List<string> { user.Email }, "Email Verification", "Verify your email");

            return 1;
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
                if (existUser == null) throw new CustomException(404, "User or Pasword wrong");
            }
            var result = await _userManager.CheckPasswordAsync(existUser, loginDto.Password);
            if (!result) throw new CustomException(404, "User or Pasword wrong");
            var roles = await _userManager.GetRolesAsync(existUser);
            return _tokenService.GetToken(existUser, roles);

        }
    }
}
