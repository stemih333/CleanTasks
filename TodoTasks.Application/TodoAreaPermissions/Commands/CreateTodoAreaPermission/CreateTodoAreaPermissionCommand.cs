using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoAreaPermissions.Commands
{
    public class CreateTodoAreaPermissionCommand : BaseCommand, IRequest<int>
    {
        public int? TodoAreaId { get; set; }
    }
}
