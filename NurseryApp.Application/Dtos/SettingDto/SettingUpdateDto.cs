using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.SettingDto
{
    public class SettingUpdateDto
    {
        public string? Value { get; set; }
        public IFormFile? File { get; set; }
    }
    public class SettingUpdateDtoValidator : AbstractValidator<SettingUpdateDto>
    {
        public SettingUpdateDtoValidator()
        {


            RuleFor(s => s).Custom((s, c) =>
            {
                if (s.File != null && s.File.Length / 1024 > 300)
                {
                    c.AddFailure("File", "File size must less than 300");
                }
                if (s.File != null && !s.File.ContentType.Contains("image/"))
                {
                    c.AddFailure("File", "File  must only image");
                }
            });

        }
    }
}
