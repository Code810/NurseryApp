using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.Property(p => p.Adress).IsRequired().HasMaxLength(100);
            builder.Property(p => p.RelationToStudent).IsRequired().HasMaxLength(15);
            builder.HasMany(p => p.Students).WithOne(t => t.Parent).HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
