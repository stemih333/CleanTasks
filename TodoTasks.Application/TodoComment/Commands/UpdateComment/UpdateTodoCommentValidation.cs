using FluentValidation;

namespace CleanTodoTasks.Application.TodoComment.Commands
{
    public class UpdateTodoCommentValidation : AbstractValidator<UpdateTodoCommentCommand>
    {
        public UpdateTodoCommentValidation()
        {
            RuleFor(_ => _.CommentId).NotEmpty();
            RuleFor(_ => _.Value).NotEmpty().MaximumLength(300);
            RuleFor(_ => _.UserId).NotEmpty().MaximumLength(50);
        }
    }
}
