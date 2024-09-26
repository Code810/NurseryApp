using FluentValidation;

namespace NurseryApp.Application.Dtos.Contact
{
    public class ContactCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Message { get; set; }
    }

    public class ContactCreateDtoValidator : AbstractValidator<ContactCreateDto>
    {
        public ContactCreateDtoValidator()
        {
            RuleFor(c => c.FullName).NotNull().MaximumLength(50);
            RuleFor(c => c.Adress).NotNull().MaximumLength(120);
            RuleFor(c => c.Email).NotNull().EmailAddress();
            RuleFor(c => c.Message).NotNull().MaximumLength(500);
        }
    }
}
