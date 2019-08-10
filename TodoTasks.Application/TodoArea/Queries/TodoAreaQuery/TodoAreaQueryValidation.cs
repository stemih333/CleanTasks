using FluentValidation;

namespace TodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaQueryValidation : AbstractValidator<TodoAreaQuery>
    {
        public TodoAreaQueryValidation()
        {
            RuleFor(_ => _.UserAreas)
                .NotEmpty()
                .WithMessage("No Todo area ID:s have been sent with the query request. Its possible that the user is not assigned to a Todo area.");
        }
    }
}
