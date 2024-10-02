using NurseryApp.Application.Dtos.Auth;
using NurseryApp.Core.Entities;

namespace NurseryApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AppUser> Register(RegisterDto registerDto);
        Task<int> ConfirmEmail(string email, string token);
        Task CreateRoles();
        Task<string> Login(LoginDto loginDto);
        Task<string> ForgetPassword(string email);
        Task<int> ResetPasswordAsync(string email, string token, ResetPasswordDto resetPasswordDto);
    }
}
