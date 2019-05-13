using CleanTodoTasks.DataAccess.Extensions;
using CleanTodoTasks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTodoTasks.DataAccess.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<TodoTag>
    {
        public void Configure(EntityTypeBuilder<TodoTag> builder)
        {
            builder.HasKey(_ => _.TagId);
            builder.Property(_ => _.TagId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Value)
                .IsRequired()
                .HasMaxLength(25);

            builder.HasIndex(_ => new { _.Value, _.TodoId })
                .IsUnique();

            builder.SetAllAuditableProperties();
        }
    }
}
