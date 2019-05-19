using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace TodoTasks.WebAPI.Filters
{
    public class ValidateViewModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.Any())
            {
                throw new NullReferenceException("Action arguments cannot be null.");
            }
            
            base.OnActionExecuting(context);
        }
    }
}
