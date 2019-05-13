using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.Interfaces
{
    public interface ITodoDbContext
    {
        DbSet<Domain.Entities.TodoComment> Comments { get; set; }
        DbSet<Domain.Entities.TodoTag> Tags { get; set; }
        DbSet<Domain.Entities.Todo> Todos { get; set; }
        DbSet<Domain.Entities.TodoArea> TodoAreas { get; set; }
        DbSet<Domain.Entities.Attachment> Attachments { get; set; }
        DbSet<Domain.Entities.TodoAreaPermission> TodoAreaPermissions { get; set; }

        Task<int> SaveAuditableChangesAsync(string userName, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
