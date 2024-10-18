using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.TeacherDto
{
    public class TeacherUpdateDto
    {
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }
        public IFormFile? File { get; set; }
    }
    public class TeacherUpdateDtoValidator : AbstractValidator<TeacherUpdateDto>
    {
        public TeacherUpdateDtoValidator()
        {


            RuleFor(t => t).Custom((t, c) =>
            {
                if (t.File != null)
                {
                    // Check if the file size is greater than 300 KB
                    if (t.File.Length / 1024 > 300)
                    {
                        c.AddFailure("File", "File size must be less than 300 KB");
                    }

                    // Check if the file is not an image
                    if (!t.File.ContentType.Contains("image/"))
                    {
                        c.AddFailure("File", "File must only be an image");
                    }
                }
            });

        }
    }
}
