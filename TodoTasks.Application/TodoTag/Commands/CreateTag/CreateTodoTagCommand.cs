using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoTag.Commands
{
    public class CreateTodoTagCommand : BaseCommand, IRequest<int>
    {
        public int? TodoId { get; set; }
        public string Value { get; set; }
    }
}
