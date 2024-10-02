using NurseryApp.Application.Dtos.ParentDto;

namespace NurseryApp.Application.Dtos.Auth
{
    public class RegisterWithParentDto
    {
        public RegisterDto RegisterDto { get; set; }
        public ParentCreateDto? ParentCreateDto { get; set; }
    }
}
