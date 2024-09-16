using NurseryApp.Core.Entities;

namespace NurseryApp.Application.Interfaces
{
    public interface ITokenService
    {
        string GetToken(AppUser user, IList<string> roles);
    }
}
