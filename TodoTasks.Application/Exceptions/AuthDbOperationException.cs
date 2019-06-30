using System;
using System.Collections.Generic;

namespace TodoTasks.Application.Exceptions
{
    public class AuthDbOperationException : Exception
    {
        public readonly IEnumerable<string> Errors;

        public AuthDbOperationException(string message, IEnumerable<string> errors = null) : base(message)
        {
            Errors = errors;
        }
    }
}
