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

            RuleFor(s => s).Custom((s, c) =>
            {
                if (s.File != null)
                {
                    if (s.File.Length / 1024 > 300)
                    {
                        c.AddFailure("File", "File size must be less than 300 KB");
                    }

                    if (!s.File.ContentType.Contains("image/"))
                    {
                        c.AddFailure("File", "File must only be an image");
                    }
                }
            });

        }
    }
}
