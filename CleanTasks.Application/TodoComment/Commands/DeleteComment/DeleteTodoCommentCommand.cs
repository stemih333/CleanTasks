using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.TodoComment.Commands
{
    public class DeleteTodoCommentCommand : BaseCommand, IRequest
    {
        public int? CommentId { get; set; }
    }
}
