namespace NurseryApp.Application.Dtos.AppUser
{
    public class AppUserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? NewPassword { get; set; }
        public string? NewRePassword { get; set; }
        public string Password { get; set; }
    }
}
