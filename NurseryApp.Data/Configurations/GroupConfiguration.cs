﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurseryApp.Core.Entities;

namespace NurseryApp.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Name).IsRequired().HasMaxLength(10);
            builder.Property(g => g.RoomNumber).IsRequired();
            builder.Property(g => g.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasKey(g => g.Id);
            builder.HasOne(g => g.Teacher).WithOne(t => t.Group).HasForeignKey<Group>(t => t.TeacherId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(g => g.HomeWorks).WithOne(t => t.Group).HasForeignKey(t => t.GroupId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
