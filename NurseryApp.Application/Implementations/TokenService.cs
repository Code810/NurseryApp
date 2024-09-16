using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NurseryApp.Application.Interfaces;
using NurseryApp.Application.Setting;
using NurseryApp.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NurseryApp.Application.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly JwtSetting _jwtSetting;

        public TokenService(IOptions<JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }

        public string GetToken(AppUser user, IList<string> roles)
        {
            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.UTF8.GetBytes(_jwtSetting.SecretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ci.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddMinutes(20),
                Subject = ci,
                Audience = _jwtSetting.Audience,
                Issuer = _jwtSetting.Issuer,
            };
            var token = handler.WriteToken(handler.CreateToken(tokenDescriptor));
            return token;
        }
    }
}
