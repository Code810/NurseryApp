using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NurseryApp.Application.Dtos.StudentDto;
using NurseryApp.Application.Implementations;
using NurseryApp.Application.Interfaces;
using NurseryApp.Application.Profiles;
using NurseryApp.Application.Setting;
using NurseryApp.Core.Entities;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;
using NurseryApp.Data.Implementations;
using System.Text;

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
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<NurseryAppContext>().AddDefaultTokenProviders();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

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
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<IGroupMessageService, GroupMessageService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
    });
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:SecretKey").Value)),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    //ClockSkew = TimeSpan.Zero

                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        // Check if the request is for SignalR hub, extract the token from the query string
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chathub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });




            services.Configure<JwtSetting>(config.GetSection("Jwt"));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
            services.AddSignalR();

        }
    }
}
