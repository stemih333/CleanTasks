using FluentValidation;

namespace CleanTasks.Application.TodoComment.Commands
{
    public class CreateTodoCommentValidation : AbstractValidator<CreateTodoCommentCommand>
    {
        public CreateTodoCommentValidation()
        {
            RuleFor(_ => _.TodoId).NotEmpty();
            RuleFor(_ => _.Value).NotEmpty().MaximumLength(300);
        }
    }
}
