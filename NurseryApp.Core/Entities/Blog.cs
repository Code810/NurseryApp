namespace NurseryApp.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string FileName { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
