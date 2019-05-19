using MediatR;

namespace TodoTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionCommand : IRequest
    {
        public int? Id { get; set; }
    }
}
