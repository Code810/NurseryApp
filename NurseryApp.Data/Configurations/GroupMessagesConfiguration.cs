using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class GroupMessagesConfiguration : IEntityTypeConfiguration<GroupMessage>
    {
        public void Configure(EntityTypeBuilder<GroupMessage> builder)
        {
            builder.HasKey(gm => gm.Id);

            builder.HasOne(gm => gm.Group)
                   .WithMany(g => g.GroupMessages)
                   .HasForeignKey(gm => gm.GroupId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(gm => gm.Message)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(gm => gm.SentAt)
                   .IsRequired();
        }
    }
}
