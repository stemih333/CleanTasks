using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTasks.DataAccess.Extensions;
using TodoTasks.Domain.Entities;

namespace TodoTasks.DataAccess.Configuration
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
                .HasMaxLength(50);

            builder.SetAllAuditableProperties();
        }
    }
}
