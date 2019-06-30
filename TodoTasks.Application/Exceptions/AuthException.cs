using System;
using System.Collections.Generic;

namespace TodoTasks.Application.Exceptions
{
    public class AuthException : Exception
    {
        public readonly IEnumerable<string> Errors;
        public AuthException(string message, IEnumerable<string> errors = null) : base(message)
        {
            Errors = errors;
        }
    }
}
