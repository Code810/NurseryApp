namespace NurseryApp.Core.Entities
{
    public class GroupMessage : BaseEntity
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
