using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class HomeWorkSubmissionConfiguration : IEntityTypeConfiguration<HomeWorkSubmission>
    {
        public void Configure(EntityTypeBuilder<HomeWorkSubmission> builder)
        {
            builder.Property(g => g.SubmissionDate).IsRequired();
            builder.Property(g => g.Grade).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasKey(g => g.Id);
        }
    }
}
