using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.Blog
{
    public class BlogCreateDto
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public IFormFile File { get; set; }
        public string AppUserId { get; set; }

        public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
        {
            public BlogCreateDtoValidator()
            {


                RuleFor(b => b).Custom((b, c) =>
                {
                    if (b.File != null && b.File.Length / 1024 > 300)
                    {
                        c.AddFailure("File", "File size must less than 300");
                    }
                    if (!(b.File != null && b.File.ContentType.Contains("image/")))
                    {
                        c.AddFailure("File", "File  must only image");
                    }
                });

            }
        }
    }
}
