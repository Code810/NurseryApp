namespace NurseryApp.Application.Dtos.Comment
{
    public class CommentCreateDto
    {
        public string Message { get; set; }
        public int BlogId { get; set; }
        public string AppUserId { get; set; }
    }
}
