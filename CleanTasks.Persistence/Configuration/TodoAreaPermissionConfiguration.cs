using CleanTasks.Domain.Entities;
using CleanTasks.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTasks.Persistence.Configuration
{
    public class TodoAreaPermissionConfiguration : IEntityTypeConfiguration<TodoAreaPermission>
    {
        public void Configure(EntityTypeBuilder<TodoAreaPermission> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.UserId)
                .IsRequired()
                .HasMaxLength(25);

            builder.SetAllAuditableProperties();
        }
    }
}
