using FluentValidation;

namespace TodoTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionValidation : AbstractValidator<DeleteTodoAreaPermissionCommand>
    {
        public DeleteTodoAreaPermissionValidation()
        {
            RuleFor(_ => _.Id).NotEmpty().GreaterThan(0);
        }
    }
}
