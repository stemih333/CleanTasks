using CleanTodoTasks.Domain.Entities;
using CleanTodoTasks.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionHandler : IRequestHandler<DeleteTodoAreaPermissionCommand>
    {
        private readonly TodoDbContext _todoDbContext;

        public DeleteTodoAreaPermissionHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(DeleteTodoAreaPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToDelete = new TodoAreaPermission { Id = request.Id.Value };

            _todoDbContext.TodoAreaPermissions.Remove(permissionToDelete);

            await _todoDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
