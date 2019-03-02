﻿using CleanTasks.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTasks.Persistence.Extensions
{
    public static class AuditableEntityConfigurationExtensions
    {
        public static EntityTypeBuilder<T> SetUpdatedByProperty<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        {
            builder.Property(_ => _.UpdatedBy)
                .IsRequired()
                .HasMaxLength(50);

            return builder;
        }

        public static EntityTypeBuilder<T> SetCreatedByProperty<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        {
            builder.Property(_ => _.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            return builder;
        }

        public static EntityTypeBuilder<T> SetAllAuditableProperties<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        => builder
            .SetCreatedByProperty()
            .SetUpdatedByProperty();
    }
}