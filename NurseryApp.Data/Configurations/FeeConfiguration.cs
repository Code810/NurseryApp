using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.Property(f => f.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(f => f.DueDate).IsRequired();
            builder.Property(f => f.StudentId).IsRequired();
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Student).WithMany(s => s.Fees).HasForeignKey(s => s.StudentId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
