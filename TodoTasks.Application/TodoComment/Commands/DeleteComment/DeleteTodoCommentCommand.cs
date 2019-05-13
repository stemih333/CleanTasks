using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.TodoComment.Commands
{
    public class DeleteTodoCommentCommand : BaseCommand, IRequest
    {
        public int? CommentId { get; set; }
    }
}
