using AutoMapper;
using Microsoft.AspNetCore.Http;
using NurseryApp.Application.Dtos.AppUser;
using NurseryApp.Application.Dtos.AttenDanceDto;
using NurseryApp.Application.Dtos.Banner;
using NurseryApp.Application.Dtos.Blog;
using NurseryApp.Application.Dtos.Comment;
using NurseryApp.Application.Dtos.Contact;
using NurseryApp.Application.Dtos.FeeDto;
using NurseryApp.Application.Dtos.GroupDto;
using NurseryApp.Application.Dtos.HomeWork;
using NurseryApp.Application.Dtos.HomeWorkSubmission;
using NurseryApp.Application.Dtos.ParentDto;
using NurseryApp.Application.Dtos.SettingDto;
using NurseryApp.Application.Dtos.StudentDto;
using NurseryApp.Application.Dtos.TeacherDto;
using NurseryApp.Application.Extensions;
using NurseryApp.Core.Entities;

namespace NurseryApp.Application.Profiles
{
    public class MapperProfile : Profile
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public MapperProfile(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            var urlBuilder = new UriBuilder(
                _contextAccessor.HttpContext.Request.Scheme,
                _contextAccessor.HttpContext.Request.Host.Host,
                _contextAccessor.HttpContext.Request.Host.Port.Value
                );
            var url = urlBuilder.Uri.AbsoluteUri;
            //AttenDance
            CreateMap<AttenDanceCreateDto, AttenDance>();
            CreateMap<AttenDance, AttenDanceReturnDto>();
            //Fee
            CreateMap<Fee, FeeReturnDto>();

            //Group
            CreateMap<GroupCreateDto, Group>();
            CreateMap<Group, GroupReturnDto>();
            CreateMap<GroupUpdateDto, Group>();

            //Comment
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<Comment, CommentReturnDto>();
            CreateMap<CommentUpdateDto, Comment>();

            //HomeWork
            CreateMap<HomeWorkCreateDto, HomeWork>();
            CreateMap<HomeWork, HomeWorkReturnDto>();
            CreateMap<HomeWorkUpdateDto, HomeWork>();

            //HomeWorkSubmission
            CreateMap<HomeWorkSubmissionCreateDto, HomeWorkSubmission>();
            CreateMap<HomeWorkSubmission, HomeWorkSubmissionReturnDto>();
            CreateMap<HomeWorkSubmissionUpdateDto, HomeWorkSubmission>();
            //Parent
            CreateMap<ParentCreateDto, Parent>();
            CreateMap<Parent, ParentReturnDto>();
            CreateMap<ParentUpdateDto, Parent>();

            //Student
            CreateMap<StudentCreateDto, Student>()
              .ForMember(s => s.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/student")));
            CreateMap<Student, StudentReturnDto>().ForMember(d => d.FileName, map => map.MapFrom(s => url + "images/student/" + s.FileName))
                .ForMember(d => d.Fees, map => map.MapFrom(b => b.Fees.OrderByDescending(f => f.DueDate)))
                .ForMember(d => d.Group, map => map.MapFrom(b => b.Group));
            //CreateMap<StudentUpdateDto, Student>()
            // .ForMember(s => s.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/student")));

            CreateMap<StudentUpdateDto, Student>()
    .ForMember(s => s.FileName, opt => opt.MapFrom((src, dest) =>
    {
        if (src.File != null)
        {
            return src.File.Save(Directory.GetCurrentDirectory(), "images/student");
        }
        return dest.FileName;
    }))
    .ForMember(s => s.FirstName, opt => opt.Condition(src => src.FirstName != null))
    .ForMember(s => s.LastName, opt => opt.Condition(src => src.LastName != null))
    .ForMember(s => s.DateOfBirth, opt => opt.Condition(src => src.DateOfBirth != default(DateTime)))
    .ForMember(s => s.Gender, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Gender)));



            //Teacher
            CreateMap<TeacherCreateDto, Teacher>()
              .ForMember(t => t.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/teacher")));
            CreateMap<Teacher, TeacherReturnDto>().ForMember(d => d.FileName, map => map.MapFrom(t => url + "images/teacher/" + t.FileName));
            CreateMap<TeacherUpdateDto, Teacher>()
             .ForMember(t => t.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/teacher")));

            //AppUser
            CreateMap<AppUserUpdateDto, AppUser>()
           .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<AppUser, AppUserReturnDto>();

            //Blog
            CreateMap<BlogCreateDto, Blog>()
              .ForMember(b => b.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/blogs")));

            CreateMap<Blog, BlogReturnDto>()
    .ForMember(d => d.FileName, map => map.MapFrom(b => url + "images/blogs/" + b.FileName))
    .ForMember(d => d.Comments, map => map.MapFrom(b => b.Comments));

            CreateMap<BlogUpdateDto, Blog>()
             .ForMember(b => b.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/blogs")));

            //Banner
            CreateMap<BannerCreateDto, Banner>()
              .ForMember(b => b.LeftFileName, map => map.MapFrom(d => d.LeftFile.Save(Directory.GetCurrentDirectory(), "images/banner")))
              .ForMember(b => b.RightFileName, map => map.MapFrom(d => d.RightFile.Save(Directory.GetCurrentDirectory(), "images/banner")))
              .ForMember(b => b.BottomFileName, map => map.MapFrom(d => d.BottomFile.Save(Directory.GetCurrentDirectory(), "images/banner")));

            CreateMap<Banner, BannerReturnDto>()
                .ForMember(d => d.LeftFileName, map => map.MapFrom(B => url + "images/banner/" + B.LeftFileName))
                .ForMember(d => d.RightFileName, map => map.MapFrom(B => url + "images/banner/" + B.RightFileName))
                .ForMember(d => d.BottomFileName, map => map.MapFrom(B => url + "images/banner/" + B.BottomFileName));


            CreateMap<BannerUpdateDto, Banner>()
             .ForMember(b => b.LeftFileName, map => map.MapFrom(d => d.LeftFile.Save(Directory.GetCurrentDirectory(), "images/banner")))
             .ForMember(b => b.RightFileName, map => map.MapFrom(d => d.RightFile.Save(Directory.GetCurrentDirectory(), "images/banner")))
             .ForMember(b => b.BottomFileName, map => map.MapFrom(d => d.BottomFile.Save(Directory.GetCurrentDirectory(), "images/banner")));

            //Contact
            CreateMap<ContactCreateDto, Contact>();
            CreateMap<Contact, ContactReturnDto>();

            //Setting
            CreateMap<SettingCreateDto, Settings>()
       .ForMember(b => b.Value, map => map.MapFrom((src, dest) =>
       {
           if (src.Value != null && src.File == null)
           {
               return src.Value;
           }
           else if (src.File != null && src.Value == null)
           {
               var fileName = src.File.Save(Directory.GetCurrentDirectory(), "images/settings");
               return fileName;
           }
           return dest.Value;
       }));



            CreateMap<Settings, SettingReturnDto>()
    .ForMember(d => d.Value, map => map.MapFrom(s =>
        (s.Value != null && s.Value.IsImage())
        ? $"{url}/images/settings/{s.Value}"
        : s.Value));

            CreateMap<SettingUpdateDto, Settings>()
       .ForMember(b => b.Value, map => map.MapFrom((src, dest) =>
       {
           if (src.Value != null && src.File == null)
           {
               return src.Value;
           }
           else if (src.File != null && src.File.ContentType.Contains("image/"))
           {
               var fileName = src.File.Save(Directory.GetCurrentDirectory(), "images/settings");
               return fileName;
           }
           return dest.Value;
       }));

        }



    }


}
