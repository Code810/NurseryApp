using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using NurseryApp.Data.Data;

namespace NurseryApp.Api
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<NurseryAppContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly("NurseryApp.Data"));
            });
            services.AddHttpContextAccessor();
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
        }
    }
}
