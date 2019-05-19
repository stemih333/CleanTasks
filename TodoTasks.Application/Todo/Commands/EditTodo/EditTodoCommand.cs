using TodoTasks.Application.BaseClasses;
using TodoTasks.Common;
using MediatR;

namespace TodoTasks.Application.Todo.Commands
{
    public class EditTodoCommand : BaseCommand, IRequest<int>
    {
        public int? TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public TodoType? Type { get; set; }
        public TodoReason? CloseReasonId { get; set; }
        public TodoStatus? TodoStatusId { get; set; }
        public int? TodoAreaId { get; set; }
        public bool Notify { get; set; }
    }
}
