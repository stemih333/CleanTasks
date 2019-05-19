using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoTasks.Common;
using TodoTasks.DataAccess.Extensions;
using TodoTasks.Domain.Entities;

namespace TodoTasks.DataAccess.Configuration
{
    class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(_ => _.TodoId);
            builder.Property(_ => _.TodoId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(_ => _.Description)
                .IsRequired()
                .HasMaxLength(10000);

            builder.Property(_ => _.AssignedTo)
                .HasMaxLength(50);

            builder.Property(_ => _.Status)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<TodoStatus>());

            builder.Property(_ => _.Type)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<TodoType>());

            builder.Property(_ => _.CloseReason)
                .HasConversion(new EnumToStringConverter<TodoReason>());

            builder.Property(_ => _.Rowversion)
                .IsRowVersion();

            builder.Property(_ => _.TodoAreaId)
                .IsRequired();

            builder.HasMany(_ => _.LinkedTodos)
                .WithOne(_ => _.LinkedTodo)
                .HasForeignKey(_ => _.LinkedTodoId);

            builder.HasIndex(new[] { "Title", "TodoAreaId" })
                .IsUnique();

            builder.SetAllAuditableProperties();
        }
    }
}
