using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTasks.DataAccess.Extensions;
using TodoTasks.Domain.Entities;
namespace TodoTasks.DataAccess.Configuration
{
    public class TodoAreaConfiguration : IEntityTypeConfiguration<TodoArea>
    {
        public void Configure(EntityTypeBuilder<TodoArea> builder)
        {
            builder.HasKey(_ => _.TodoAreaId);
            builder.Property(_ => _.TodoAreaId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(_ => _.Name).IsUnique();

            builder.SetAllAuditableProperties();
        }
    }
}
