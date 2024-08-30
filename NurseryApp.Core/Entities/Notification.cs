namespace NurseryApp.Core.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
