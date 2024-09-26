namespace NurseryApp.Application.Dtos.Blog
{
    public class BlogListDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<BlogReturnDto> Items { get; set; }
    }
}
