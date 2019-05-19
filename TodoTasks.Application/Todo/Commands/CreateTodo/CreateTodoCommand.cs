using TodoTasks.Application.BaseClasses;
using TodoTasks.Common;
using MediatR;

namespace TodoTasks.Application.Todo.Commands
{
    public class CreateTodoCommand : BaseCommand, IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public TodoType? Type { get; set; }
        public int? TodoAreaId { get; set; }
        public bool Notify { get; set; }
    }
}
