using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoArea.Commands
{
    public class DeleteTodoAreaCommand : BaseCommand, IRequest
    {
        public int? TodoAreaId { get; set; }
    }
}
