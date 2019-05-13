using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanTodoTasks.Application.Exceptions
{
    public class TodoValidationException : Exception
    {
        public readonly List<ValidationError> Errors;

        public TodoValidationException(List<ValidationFailure> failures) : base()
        {
            Errors = failures
                .Select(_ => new ValidationError {
                    ErrorMessage = _.ErrorMessage,
                    Property = _.PropertyName,
                    AttemptedValue = _.AttemptedValue
                })
                .ToList();
        }

        public class ValidationError {
            public string Property { get; set; }
            public string ErrorMessage { get; set; }
            public object AttemptedValue { get; set; }
        }
    }
}
