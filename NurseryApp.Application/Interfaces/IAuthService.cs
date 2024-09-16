using NurseryApp.Application.Dtos.Auth;

namespace NurseryApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<int> Register(RegisterDto registerDto, string scheme, string host);
        Task<int> ConfirmEmail(string email, string token);
        Task CreateRoles();
        Task<string> Login(LoginDto loginDto);
    }
}
