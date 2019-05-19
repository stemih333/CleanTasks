using FluentValidation;

namespace TodoTasks.Application.TodoTag.Commands
{
    public class CreateTodoTagValidation : AbstractValidator<CreateTodoTagCommand>
    {
        public CreateTodoTagValidation()
        {
            RuleFor(_ => _.TodoId).NotEmpty();
            RuleFor(_ => _.Value).NotEmpty().MaximumLength(25);
            RuleFor(_ => _.UserId).NotEmpty().MaximumLength(50);
        }
    }
}
