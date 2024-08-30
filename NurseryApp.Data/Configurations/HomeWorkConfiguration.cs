using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class HomeWorkConfiguration : IEntityTypeConfiguration<HomeWork>
    {
        public void Configure(EntityTypeBuilder<HomeWork> builder)
        {
            builder.Property(g => g.Title).IsRequired().HasMaxLength(20);
            builder.Property(g => g.Description).IsRequired();
            builder.Property(g => g.DueDate).IsRequired();
            builder.HasKey(g => g.Id);
            builder.HasMany(g => g.HomeWorkSubmissions).WithOne(t => t.HomeWork).HasForeignKey(t => t.HomeWorkId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
