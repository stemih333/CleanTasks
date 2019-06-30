using FluentValidation;

namespace TodoTasks.Application.AppUser.Commands
{
    public class SetAreaPermissionValidation : AbstractValidator<SetAreaPermissionCommand>
    {
        public SetAreaPermissionValidation()
        {
            RuleFor(_ => _.Permission).NotEmpty().MaximumLength(50);
            RuleFor(_ => _.Username).NotEmpty().MaximumLength(50);
        }
    }
}
