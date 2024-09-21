using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NurseryApp.Core.Entities;
using System.Reflection;

namespace NurseryApp.Data.Data
{
    public class NurseryAppContext : IdentityDbContext<AppUser>
    {
        public NurseryAppContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AttenDance> AttenDances { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<HomeWorkSubmission> HomeWorkSubmissions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Settings> settings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


    }
}
