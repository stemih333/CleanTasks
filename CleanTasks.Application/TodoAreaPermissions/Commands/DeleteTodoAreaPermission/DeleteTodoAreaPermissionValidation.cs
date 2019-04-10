using FluentValidation;

namespace CleanTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionValidation : AbstractValidator<DeleteTodoAreaPermissionCommand>
    {
        public DeleteTodoAreaPermissionValidation()
        {
            RuleFor(_ => _.Id).NotEmpty().GreaterThan(0);
        }
    }
}
