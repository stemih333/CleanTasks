using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanTasks.RazorGUI.Attributes
{
    public class ValidAreaAttribute : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var client = context.HttpContext.RequestServices.GetService<ITodoAreaApiClient>();

            if (client != null)
            {
                var routes = context.RouteData.Values;

                if(routes["id"] is string id)
                {
                    if (!client.AreaExist(id).Result) throw new Exception($"Area id '{id}' does not exist.");
                }                
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }       
    }
}
