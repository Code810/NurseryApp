using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.Blog
{
    public class BlogUpdateDto
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public IFormFile? File { get; set; }
    }
}
