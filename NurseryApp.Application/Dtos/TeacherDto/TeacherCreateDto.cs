﻿using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NurseryApp.Application.Dtos.TeacherDto
{
    public class TeacherCreateDto
    {
        public string AppUserId { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public IFormFile File { get; set; }
    }
    public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidator()
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
