using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoTasks.DataAccess.Extensions;
using TodoTasks.Domain.Entities;

namespace TodoTasks.DataAccess.Configuration
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

            builder.Property(_ => _.SavedFileName)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(_ => _.Description)
                .HasMaxLength(300);

            builder.Property(_ => _.FileType)
                .HasMaxLength(100);

            builder.Property(_ => _.Size)
                .IsRequired();

            builder.HasIndex(_ => new { _.Name, _.TodoId })
                .IsUnique();

            builder.SetAllAuditableProperties();
        }
    }
}
