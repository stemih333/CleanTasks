﻿using CleanTodoTasks.DataAccess.Extensions;
using CleanTodoTasks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTodoTasks.DataAccess.Configuration
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(_ => _.AttachmentId);
            builder.Property(_ => _.AttachmentId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(_ => _.Size)
                .IsRequired();

            builder.HasIndex(_ => new { _.Name, _.TodoId })
                .IsUnique();

            builder.SetAllAuditableProperties();
        }
    }
}