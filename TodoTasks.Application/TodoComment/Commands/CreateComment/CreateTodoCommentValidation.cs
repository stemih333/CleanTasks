using FluentValidation;

namespace TodoTasks.Application.TodoComment.Commands
{
    public class CreateTodoCommentValidation : AbstractValidator<CreateTodoCommentCommand>
    {
        public CreateTodoCommentValidation()
        {
            RuleFor(_ => _.TodoId).NotEmpty();
            RuleFor(_ => _.Value).NotEmpty().MaximumLength(300);
            RuleFor(_ => _.UserId).NotEmpty().MaximumLength(50);
        }
    }
}
