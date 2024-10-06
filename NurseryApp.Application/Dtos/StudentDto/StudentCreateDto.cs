using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.StudentDto
{
    public class StudentCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int ParentId { get; set; }
        public IFormFile File { get; set; }
    }

    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(s => s.FirstName).MaximumLength(50);
            RuleFor(s => s.LastName).MaximumLength(50);
            RuleFor(s => s.DateOfBirth).Must(BeWithinAgeRange).WithMessage("The child's age must be between 2 and 7 years");

            RuleFor(s => s).Custom((s, c) =>
            {
                if (s.File != null && s.File.Length / 1024 > 300)
                {
                    c.AddFailure("File", "File size must less than 300");
                }
                if (!(s.File != null && s.File.ContentType.Contains("image/")))
                {
                    c.AddFailure("File", "File  must only image");
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
