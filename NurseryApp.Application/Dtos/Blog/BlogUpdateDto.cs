using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.Blog
{
    public class BlogUpdateDto
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public IFormFile? File { get; set; }
    }

    public class BlogUpdateDtoValidator : AbstractValidator<BlogUpdateDto>
    {
        public BlogUpdateDtoValidator()
        {


            RuleFor(t => t).Custom((t, c) =>
            {
                if (t.File != null && t.File.Length / 1024 > 300)
                {
                    c.AddFailure("File", "File size must less than 300");
                }
                if (!(t.File != null && t.File.ContentType.Contains("image/")))
                {
                    c.AddFailure("File", "File  must only image");
                }
            });

        }
    }
}
