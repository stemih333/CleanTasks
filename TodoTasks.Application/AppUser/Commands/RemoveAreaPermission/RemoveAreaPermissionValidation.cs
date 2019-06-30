using FluentValidation;

namespace TodoTasks.Application.AppUser.Commands
{
    public class RemoveAreaPermissionValidation : AbstractValidator<RemoveAreaPermissionCommand>
    {
        public RemoveAreaPermissionValidation()
        {
            RuleFor(_ => _.Permission).NotEmpty().MaximumLength(50);
            RuleFor(_ => _.Username).NotEmpty().MaximumLength(50);
        }
    }
}
