namespace NurseryApp.Application.Dtos.Comment
{
    public class CommentReturnDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string AppUserUserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
