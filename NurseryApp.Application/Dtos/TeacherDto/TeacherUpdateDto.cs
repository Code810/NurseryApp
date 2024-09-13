using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.TeacherDto
{
    public class TeacherUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile File { get; set; }
    }
    public class TeacherUpdateDtoValidator : AbstractValidator<TeacherUpdateDto>
    {
        public TeacherUpdateDtoValidator()
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
