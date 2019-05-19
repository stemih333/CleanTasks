using TodoTasks.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionHandler : IRequestHandler<DeleteTodoAreaPermissionCommand>
    {
        private readonly ITodoDbContext _todoDbContext;

        public DeleteTodoAreaPermissionHandler(ITodoDbContext todoDbContext)
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
