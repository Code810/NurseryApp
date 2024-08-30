using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(15);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(15);
            builder.Property(p => p.DateOfBirth).IsRequired();
            builder.Property(p => p.Gender).IsRequired().HasMaxLength(6);
            builder.HasMany(s => s.HomeWorkSubmissions).WithOne(p => p.Student).HasForeignKey(p => p.StudentId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
