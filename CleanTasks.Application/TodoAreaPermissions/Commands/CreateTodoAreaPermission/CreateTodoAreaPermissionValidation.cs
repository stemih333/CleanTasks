using FluentValidation;

namespace CleanTasks.Application.TodoAreaPermissions.Commands
{
    public class CreateTodoAreaPermissionValidation : AbstractValidator<CreateTodoAreaPermissionCommand>
    {
        public CreateTodoAreaPermissionValidation()
        {
            RuleFor(_ => _.TodoAreaId).NotEmpty().GreaterThan(0);
            RuleFor(_ => _.UserId).NotEmpty().MaximumLength(50);
            RuleFor(_ => _.UserName).NotEmpty().MaximumLength(50);
        }
    }
}
