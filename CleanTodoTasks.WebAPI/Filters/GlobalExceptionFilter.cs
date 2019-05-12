using CleanTodoTasks.Application.Exceptions;
using CleanTodoTasks.WebAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace CleanTodoTasks.WebAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            var httpError = new HttpError
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = GetErrorMessageFromException(context.Exception),
                StackTrace = context.Exception.StackTrace
            };

            if (exceptionType == typeof(TodoValidationException))
            {
                httpError.ValidationErrors = ((TodoValidationException)context.Exception).Errors;
                httpError.StatusCode = HttpStatusCode.BadRequest;
            }

            if (exceptionType == typeof(NotFoundException))
            {
                httpError.StatusCode = HttpStatusCode.NotFound;
            }

            if (exceptionType == typeof(NullReferenceException) || 
                exceptionType == typeof(ArgumentNullException))
            {
                httpError.StatusCode = HttpStatusCode.BadRequest;
            }

            context.Result = new JsonResult(httpError) { StatusCode = (int)httpError.StatusCode };
        }

        private string GetErrorMessageFromException(Exception ex, string errorMessage = null)
        {
            var message = errorMessage + Environment.NewLine + ex.Message;
            if (ex.InnerException == null) return message;
            return GetErrorMessageFromException(ex.InnerException, message);
        }
    }
}
