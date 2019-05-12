using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace CleanTodoTasks.RazorGUI.Exceptions
{
    public class InvalidModelStateException : Exception
    {
        public ModelStateDictionary Errors { get; set; }

        public InvalidModelStateException(ModelStateDictionary errors, string message = "") : base(message)
        {
            Errors = errors;
        }
    }
}
