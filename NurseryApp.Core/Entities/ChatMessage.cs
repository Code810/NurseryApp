namespace NurseryApp.Core.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string SenderAppUserId { get; set; }
        public string ReceiverAppUserId { get; set; }
        public AppUser SenderAppUser { get; set; }
        public AppUser ReceiverAppUser { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
