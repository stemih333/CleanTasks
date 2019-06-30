using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;
using TodoTasks.DataAccess.Configuration;
using TodoTasks.Domain.Entities;

namespace TodoTasks.DataAccess
{
    public class TodoDbContext : DbContext, ITodoDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {}

        public DbSet<TodoComment> Comments { get; set; }
        public DbSet<TodoTag> Tags { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoArea> TodoAreas { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
            modelBuilder.ApplyConfiguration(new TodoAreaConfiguration());
        }

        public Task<int> SaveAuditableChangesAsync(string userName, CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
               .Where(x => x.Entity is AuditableEntity
                   && (x.State == EntityState.Added || x.State == EntityState.Modified)).ToList();

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is AuditableEntity entity)
                {
                    DateTime now = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.Created = now; //Update 'Created' column on all inserts
                        entity.CreatedBy = userName;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.Created).IsModified = false;
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    }

                    entity.Updated = now; 
                    entity.UpdatedBy = userName; 
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
