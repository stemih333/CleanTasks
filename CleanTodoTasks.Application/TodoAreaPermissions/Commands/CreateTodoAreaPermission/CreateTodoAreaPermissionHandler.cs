using CleanTodoTasks.Domain.Entities;
using CleanTodoTasks.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoAreaPermissions.Commands
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
            await _todoDbContext.SaveAuditableChangesAsync(request.UserId);

            return newPermission.Id;
        }
    }
}
