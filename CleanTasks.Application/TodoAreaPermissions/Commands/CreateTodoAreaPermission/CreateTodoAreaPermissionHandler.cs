using CleanTasks.Domain.Entities;
using CleanTasks.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.TodoAreaPermissions.Commands
{
    public class CreateTodoAreaPermissionHandler : IRequestHandler<CreateTodoAreaPermissionCommand, int>
    {
        private readonly TodoDbContext _todoDbContext;

        public CreateTodoAreaPermissionHandler(TodoDbContext todoDbContext)
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
            await _todoDbContext.SaveAuditableChangesAsync(request.UserName);

            return newPermission.Id;
        }
    }
}
