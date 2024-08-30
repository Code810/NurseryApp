using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class AttenDanceConfiguration : IEntityTypeConfiguration<AttenDance>
    {
        public void Configure(EntityTypeBuilder<AttenDance> builder)
        {
            builder.Property(s => s.Date).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.StudentId).IsRequired();
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.Student).WithMany(g => g.AttenDances).HasForeignKey(s => s.StudentId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
