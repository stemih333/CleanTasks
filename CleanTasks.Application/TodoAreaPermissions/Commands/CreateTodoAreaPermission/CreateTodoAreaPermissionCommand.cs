using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.TodoAreaPermissions.Commands
{
    public class CreateTodoAreaPermissionCommand : BaseCommand, IRequest<int>
    {
        public string UserId { get; set; }
        public int? TodoAreaId { get; set; }
    }
}
