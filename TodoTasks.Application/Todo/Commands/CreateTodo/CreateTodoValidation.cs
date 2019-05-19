using FluentValidation;

namespace TodoTasks.Application.Todo.Commands
{
    public class CreateTodoValidation : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoValidation()
        {
            RuleFor(_ => _.Title).NotEmpty().MaximumLength(100);
            RuleFor(_ => _.Description).NotEmpty().MaximumLength(10000);
            RuleFor(_ => _.AssignedTo).MaximumLength(50);
            RuleFor(_ => _.Type).NotEmpty();
            RuleFor(_ => _.TodoAreaId).NotEmpty();
            RuleFor(_ => _.UserId).NotEmpty().MaximumLength(50);
        }
    }
}
