using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.StudentDto
{
    public class StudentUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public IFormFile? File { get; set; }
    }
    public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
    {
        public StudentUpdateDtoValidator()
        {

            RuleFor(s => s.DateOfBirth).Must(BeWithinAgeRange).WithMessage("The child's age must be between 2 and 7 years");

            RuleFor(s => s).Custom((s, c) =>
            {
                if (s.File != null)
                {
                    // Check if the file size is greater than 300 KB
                    if (s.File.Length / 1024 > 300)
                    {
                        c.AddFailure("File", "File size must be less than 300 KB");
                    }

                    // Check if the file is not an image
                    if (!s.File.ContentType.Contains("image/"))
                    {
                        c.AddFailure("File", "File must only be an image");
                    }
                }
            });

            bool BeWithinAgeRange(DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var minAgeDate = today.AddYears(-7);
                var maxAgeDate = today.AddYears(-2);

                return dateOfBirth >= minAgeDate && dateOfBirth <= maxAgeDate;
            }
        }
    }
}
