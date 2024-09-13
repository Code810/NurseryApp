using AutoMapper;
using Microsoft.AspNetCore.Http;
using NurseryApp.Application.Dtos.AttenDanceDto;
using NurseryApp.Application.Dtos.FeeDto;
using NurseryApp.Application.Dtos.GroupDto;
using NurseryApp.Application.Dtos.HomeWork;
using NurseryApp.Application.Dtos.HomeWorkSubmission;
using NurseryApp.Application.Dtos.ParentDto;
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
            CreateMap<Student, StudentReturnDto>().ForMember(d => d.FileName, map => map.MapFrom(s => url + "images/student/" + s.FileName));
            CreateMap<StudentUpdateDto, Student>()
             .ForMember(s => s.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/student")));

            //Teacher
            CreateMap<TeacherCreateDto, Teacher>()
              .ForMember(t => t.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/teacher")));
            CreateMap<Teacher, TeacherReturnDto>().ForMember(d => d.FileName, map => map.MapFrom(t => url + "images/teacher/" + t.FileName));
            CreateMap<TeacherUpdateDto, Teacher>()
             .ForMember(t => t.FileName, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images/teacher")));
        }
    }
}
