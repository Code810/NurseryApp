using FluentValidation;

namespace NurseryApp.Application.Dtos.ParentDto
{
    public class ParentCreateDto
    {
        public string Adress { get; set; }
        public string RelationToStudent { get; set; }
        public string? AppUserId { get; set; }
    }

    public class ParentCreateDtoValidator : AbstractValidator<ParentCreateDto>
    {
        public ParentCreateDtoValidator()
        {
            RuleFor(c => c.RelationToStudent).NotNull().NotEmpty().MaximumLength(5);
            RuleFor(c => c.Adress).NotNull().NotEmpty().MaximumLength(120);
        }
    }
}
