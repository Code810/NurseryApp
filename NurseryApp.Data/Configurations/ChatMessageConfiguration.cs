using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(cm => cm.Id);

            // Configure SenderAppUser relationship
            builder.HasOne(cm => cm.SenderAppUser)   // Use the navigation property for Sender
                   .WithMany()  // Assuming there's no navigation property in AppUser for sent messages
                   .HasForeignKey(cm => cm.SenderAppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure ReceiverAppUser relationship
            builder.HasOne(cm => cm.ReceiverAppUser)  // Use the navigation property for Receiver
                   .WithMany()  // Assuming there's no navigation property in AppUser for received messages
                   .HasForeignKey(cm => cm.ReceiverAppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(cm => cm.Message)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(cm => cm.SentAt)
                   .IsRequired();
        }
    }
}
