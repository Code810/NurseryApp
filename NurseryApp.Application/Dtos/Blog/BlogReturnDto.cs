using NurseryApp.Application.Dtos.Comment;

namespace NurseryApp.Application.Dtos.Blog
{
    public class BlogReturnDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string FileName { get; set; }
        public string AppUserUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CommentReturnDto> Comments { get; set; }
    }


}
