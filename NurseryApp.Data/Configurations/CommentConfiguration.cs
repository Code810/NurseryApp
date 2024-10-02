using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(n => n.Message).IsRequired().HasMaxLength(150);
            builder.HasOne(n => n.AppUser).WithMany(t => t.Comments).HasForeignKey(t => t.AppUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(n => n.Blog).WithMany(t => t.Comments).HasForeignKey(t => t.BlogId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
