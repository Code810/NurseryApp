using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.Banner
{
    public class BannerUpdateDto
    {
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string Description { get; set; }
        public IFormFile? LeftFile { get; set; }
        public IFormFile? RightFile { get; set; }
        public IFormFile? BottomFile { get; set; }
    }
    public class BannerUpdateDtoValidator : AbstractValidator<BannerUpdateDto>
    {
        public BannerUpdateDtoValidator()
        {


            RuleFor(b => b).Custom((b, c) =>
            {
                if (b.LeftFile != null && b.LeftFile.Length / 1024 > 300)
                {
                    c.AddFailure("File", "File size must less than 300");
                }
                if (!(b.LeftFile != null && b.LeftFile.ContentType.Contains("image/")))
                {
                    c.AddFailure("File", "File  must only image");
                }
            });
            RuleFor(b => b).Custom((b, c) =>
            {
                if (b.RightFile != null && b.RightFile.Length / 1024 > 300)
                {
                    c.AddFailure("File", "File size must less than 300");
                }
                if (!(b.RightFile != null && b.RightFile.ContentType.Contains("image/")))
                {
                    c.AddFailure("File", "File  must only image");
                }
            });
            RuleFor(b => b).Custom((b, c) =>
            {
                if (b.RightFile != null && b.RightFile.Length / 1024 > 300)
                {
                    c.AddFailure("File", "File size must less than 300");
                }
                if (!(b.RightFile != null && b.RightFile.ContentType.Contains("image/")))
                {
                    c.AddFailure("File", "File  must only image");
                }
            });

        }
    }
}
