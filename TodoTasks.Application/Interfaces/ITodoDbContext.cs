using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.Interfaces
{
    public interface ITodoDbContext
    {
        DbSet<TodoTasks.Domain.Entities.TodoComment> Comments { get; set; }
        DbSet<TodoTasks.Domain.Entities.TodoTag> Tags { get; set; }
        DbSet<TodoTasks.Domain.Entities.Todo> Todos { get; set; }
        DbSet<TodoTasks.Domain.Entities.TodoArea> TodoAreas { get; set; }
        DbSet<TodoTasks.Domain.Entities.Attachment> Attachments { get; set; }
        DbSet<TodoTasks.Domain.Entities.TodoAreaPermission> TodoAreaPermissions { get; set; }

        Task<int> SaveAuditableChangesAsync(string userName, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
