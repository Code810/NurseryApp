namespace NurseryApp.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Message { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
