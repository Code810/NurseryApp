using Microsoft.AspNetCore.Identity;

namespace NurseryApp.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public Parent? Parent { get; set; }
        public Teacher? Teacher { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
