using Microsoft.AspNetCore.Identity;

namespace NurseryApp.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsBlocked { get; set; }
        public string FileName { get; set; }
        public Parent? Parent { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
