using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoAreaPermissions.Commands
{
    public class CreateTodoAreaPermissionCommand : BaseCommand, IRequest<int>
    {
        public int? TodoAreaId { get; set; }
    }
}
