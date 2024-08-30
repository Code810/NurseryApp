namespace NurseryApp.Core.Entities
{
    public class Parent : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string RelationToStudent { get; set; }
        public List<Student> Students { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Notification> Notifications { get; set; }

    }
}
