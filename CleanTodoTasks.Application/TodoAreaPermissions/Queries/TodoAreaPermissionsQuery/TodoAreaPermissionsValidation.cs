using FluentValidation;

namespace CleanTodoTasks.Application.TodoAreaPermissions.Queries
{
    public class TodoAreaPermissionsValidation : AbstractValidator<TodoAreaPermissionsQuery>
    {
        public TodoAreaPermissionsValidation()
        {
            RuleFor(_ => _.UserId).MaximumLength(50);
            RuleFor(_ => _.TodoAreaId).GreaterThan(0);
        }
    }
}
