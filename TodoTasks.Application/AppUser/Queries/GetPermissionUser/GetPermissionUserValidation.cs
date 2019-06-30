using FluentValidation;

namespace TodoTasks.Application.AppUser.Queries
{
    public class GetPermissionUserValidation : AbstractValidator<GetPermissionUserQuery>
    {
        public GetPermissionUserValidation()
        {
            RuleFor(_ => _.Username).MaximumLength(50);
        }
    }
}
