using TodoTasks.Application.Interfaces;
using TodoTasks.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.TodoAreaPermissions.Commands
{
    public class CreateTodoAreaPermissionHandler : IRequestHandler<CreateTodoAreaPermissionCommand, int>
    {
        private readonly ITodoDbContext _todoDbContext;

        public CreateTodoAreaPermissionHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }
        public async Task<int> Handle(CreateTodoAreaPermissionCommand request, CancellationToken cancellationToken)
        {
            var newPermission = new TodoAreaPermission
            {
                UserId = request.UserId,
                TodoAreaId = request.TodoAreaId
            };

            _todoDbContext.TodoAreaPermissions.Add(newPermission);
            await _todoDbContext.SaveAuditableChangesAsync(request.UserId);

            return newPermission.Id;
        }
    }
}
