using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoComment.Commands
{
    public class CreateTodoCommentCommand : BaseCommand, IRequest<int>
    {
        public int? TodoId { get; set; }
        public string Value { get; set; }
    }
}
