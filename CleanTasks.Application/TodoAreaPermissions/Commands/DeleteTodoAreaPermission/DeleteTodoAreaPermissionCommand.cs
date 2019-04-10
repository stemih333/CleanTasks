using MediatR;

namespace CleanTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionCommand : IRequest
    {
        public int? Id { get; set; }
    }
}
