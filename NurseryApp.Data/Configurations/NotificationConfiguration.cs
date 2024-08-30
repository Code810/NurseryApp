using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(n => n.Message).IsRequired().HasMaxLength(150);
            builder.HasOne(n => n.Teacher).WithMany(t => t.Notifications).HasForeignKey(t => t.TeacherId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(n => n.Parent).WithMany(t => t.Notifications).HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
