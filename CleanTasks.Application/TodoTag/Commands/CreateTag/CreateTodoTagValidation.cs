using FluentValidation;

namespace CleanTasks.Application.TodoTag.Commands
{
    public class CreateTodoTagValidation : AbstractValidator<CreateTodoTagCommand>
    {
        public CreateTodoTagValidation()
        {
            RuleFor(_ => _.TodoId).NotEmpty();
            RuleFor(_ => _.Value).NotEmpty().MaximumLength(25);
        }
    }
}
