using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoComment.Commands
{
    public class CreateTodoCommentCommand : BaseCommand, IRequest<int>
    {
        public int? TodoId { get; set; }
        public string Value { get; set; }
    }
}
