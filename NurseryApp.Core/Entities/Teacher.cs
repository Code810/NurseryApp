namespace NurseryApp.Core.Entities
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Group Group { get; set; }
        public string AppUserId { get; set; }
        public string FileName { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }
        public AppUser AppUser { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
