using MediatR;

namespace CleanTodoTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionCommand : IRequest
    {
        public int? Id { get; set; }
    }
}
