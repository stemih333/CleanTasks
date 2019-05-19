using FluentValidation;

namespace TodoTasks.Application.Todo.Queries
{
    public class TodoFilterSearchQueryValidation : AbstractValidator<TodoFilterSearchQuery>
    {
        public TodoFilterSearchQueryValidation()
        {
            RuleFor(_ => _.CurrentPage).NotEmpty().GreaterThan(0);
            RuleFor(_ => _.PageSize).NotEmpty().GreaterThan(0);
            RuleFor(_ => _.TodoAreaId).NotEmpty().GreaterThan(0);
            RuleFor(_ => _.FilterProperty).NotEmpty().When(_ => !string.IsNullOrEmpty(_.FilterValue));
            RuleFor(_ => _.FilterOperator).NotEmpty().When(_ => !string.IsNullOrEmpty(_.FilterValue));
            RuleFor(_ => _.SortPropery).NotEmpty().When(_ => !string.IsNullOrEmpty(_.SortOrder));
            RuleFor(_ => _.SortOrder).NotEmpty().When(_ => !string.IsNullOrEmpty(_.SortPropery));
        }
    }
}
