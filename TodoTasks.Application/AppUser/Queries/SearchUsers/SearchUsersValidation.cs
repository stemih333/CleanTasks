using FluentValidation;

namespace TodoTasks.Application.AppUser.Queries
{
    public class SearchUsersValidation : AbstractValidator<SearchUsersQuery>
    {
        public SearchUsersValidation()
        {
            RuleFor(_ => _.ClaimType).MaximumLength(100);
            RuleFor(_ => _.ClaimValue).MaximumLength(100);
        }
    }
}
