using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.TodoComment.Commands
{
    public class DeleteTodoCommentCommand : BaseCommand, IRequest
    {
        public int? CommentId { get; set; }
    }
}
