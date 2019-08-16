using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;

namespace TodoTasks.WebAPI.Filters
{
    public class LoadingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Thread.Sleep(2000);
        }
    }
}
