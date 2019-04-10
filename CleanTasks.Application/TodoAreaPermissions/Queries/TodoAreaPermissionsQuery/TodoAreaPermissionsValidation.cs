using FluentValidation;

namespace CleanTasks.Application.TodoAreaPermissions.Queries
{
    public class TodoAreaPermissionsValidation : AbstractValidator<TodoAreaPermissionsQuery>
    {
        public TodoAreaPermissionsValidation()
        {
            RuleFor(_ => _.UserId).MaximumLength(25);
            RuleFor(_ => _.TodoAreaId).GreaterThan(0);
        }
    }
}
