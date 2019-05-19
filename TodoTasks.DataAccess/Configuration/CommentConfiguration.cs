using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTasks.DataAccess.Extensions;
using TodoTasks.Domain.Entities;

namespace TodoTasks.DataAccess.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<TodoComment>
    {
        public void Configure(EntityTypeBuilder<TodoComment> builder)
        {
            builder.HasKey(_ => _.CommentId);
            builder.Property(_ => _.CommentId)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.TodoId)
                .IsRequired();

            builder.Property(_ => _.Value)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasIndex(_ => _.Value);

            builder.SetAllAuditableProperties();
        }
    }
}
