using FluentValidation;

namespace CleanTasks.Application.TodoComment.Commands
{
    public class DeleteTodoCommentValidation : AbstractValidator<DeleteTodoCommentCommand>
    {
        public DeleteTodoCommentValidation()
        {
            RuleFor(_ => _.CommentId).NotEmpty();
        }
    }
}
