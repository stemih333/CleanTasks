using FluentValidation;

namespace TodoTasks.Application.TodoComment.Commands
{
    public class DeleteTodoCommentValidation : AbstractValidator<DeleteTodoCommentCommand>
    {
        public DeleteTodoCommentValidation()
        {
            RuleFor(_ => _.CommentId).NotEmpty();
        }
    }
}
