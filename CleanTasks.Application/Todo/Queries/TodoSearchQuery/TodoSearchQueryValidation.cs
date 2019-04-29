using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTasks.Application.Todo.Queries
{
    public class TodoSearchQueryValidation : AbstractValidator<TodoSearchQuery>
    {
        public TodoSearchQueryValidation()
        {
            RuleFor(_ => _.CurrentPage).NotEmpty().GreaterThan(0);
            RuleFor(_ => _.PageSize).NotEmpty().GreaterThan(0);
        }
    }
}
