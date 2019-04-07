using FluentValidation;

namespace CleanTasks.Application.User.Queries
{
    public class GetUsersByAreaValidation : AbstractValidator<GetUsersByAreaQuery>
    {
        public GetUsersByAreaValidation()
        {
            RuleFor(_ => _.Id).Empty().GreaterThan(0);
        }
    }
}
