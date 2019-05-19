using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoArea.Commands
{
    public class UpdateTodoAreaCommand : BaseCommand, IRequest
    {
        public int? TodoAreaId { get; set; }
        public string Name { get; set; }
    }
}
