using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoComment.Commands
{
    public class UpdateTodoCommentCommand : BaseCommand, IRequest
    {
        public int? CommentId { get; set; }
        public string Value { get; set; }
    }
}
