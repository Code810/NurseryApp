namespace NurseryApp.Core.Entities
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
