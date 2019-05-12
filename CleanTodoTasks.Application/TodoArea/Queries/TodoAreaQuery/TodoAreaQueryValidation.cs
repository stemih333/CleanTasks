using FluentValidation;

namespace CleanTodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaQueryValidation : AbstractValidator<TodoAreaQuery>
    {
        public TodoAreaQueryValidation()
        {
            RuleFor(_ => _.UserAreas)
                .Must(_ => _ != null && _.Count > 0)
                .WithMessage("No Todo area ID:s have been sent with the query request. Its possible that the user is not assigned to a Todo area.");
        }
    }
}
