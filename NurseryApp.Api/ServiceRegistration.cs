using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Application.Dtos.StudentDto;
using NurseryApp.Application.Implementations;
using NurseryApp.Application.Interfaces;
using NurseryApp.Application.Profiles;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;
using NurseryApp.Data.Implementations;

namespace NurseryApp.Api
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers()
               .ConfigureApiBehaviorOptions(opt =>
               {
                   opt.InvalidModelStateResponseFactory = context =>
                   {
                       var errors = context.ModelState.Where(e => e.Value?.Errors.Count > 0)
                       .Select(x => new Dictionary<string, string>()
                       {
            { x.Key, x.Value.Errors.First().ErrorMessage }
                       });
                       return new BadRequestObjectResult(new { message = "", errors });
                   };
               });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<NurseryAppContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly("NurseryApp.Data"));
            });
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<StudentCreateDtoValidator>();
            services.AddFluentValidationRulesToSwagger();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(opt => opt.AddProfile(new MapperProfile(new HttpContextAccessor())));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<NurseryAppContext>().AddDefaultTokenProviders();
            services.AddScoped<IAttenDanceRepository, AttenDanceRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IHomeWorkRepository, HomeWorkRepository>();
            services.AddScoped<IHomeWorkSubmissionRepository, HomeWorkSubmissionRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IParentRepository, ParentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAttenDanceService, AttenDanceService>();
            services.AddScoped<IFeeService, FeeService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IHomeWorkService, HomeWorkService>();
            services.AddScoped<IHomeWorkSubmissionService, HomeWorkSubmissionService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
        }
    }
}
