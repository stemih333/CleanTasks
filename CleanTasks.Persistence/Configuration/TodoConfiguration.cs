using CleanTasks.Domain.Entities;
using CleanTasks.Domain.Enums;
using CleanTasks.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanTasks.Persistence.Configuration
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
                .HasMaxLength(25);

            builder.Property(_ => _.Status)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<TodoStatuses>());

            builder.Property(_ => _.Type)
                .IsRequired()
                .HasConversion(new EnumToStringConverter<TodoTypes>());

            builder.Property(_ => _.CloseReason)
                .HasConversion(new EnumToStringConverter<TodoReasons>());

            builder.Property(_ => _.Rowversion)
                .IsRowVersion();

            builder.HasMany(_ => _.LinkedTodos)
                .WithOne(_ => _.LinkedTodo)
                .HasForeignKey(_ => _.LinkedTodoId);

            builder.SetAllAuditableProperties();
        }
    }
}
