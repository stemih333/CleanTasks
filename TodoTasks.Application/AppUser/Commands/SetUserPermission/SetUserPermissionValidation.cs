using FluentValidation;

namespace TodoTasks.Application.AppUser.Commands
{
    public class SetUserPermissionValidation : AbstractValidator<SetUserPermissionCommand>
    {
        public SetUserPermissionValidation()
        {
            RuleFor(_ => _.Permission).NotEmpty().MaximumLength(50);
            RuleFor(_ => _.Username).NotEmpty().MaximumLength(50);
        }
    }
}
