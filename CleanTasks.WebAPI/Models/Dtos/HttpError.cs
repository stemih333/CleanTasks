﻿using CleanTasks.Application.Exceptions;
using System.Collections.Generic;
using System.Net;

namespace CleanTasks.WebAPI.Models.Dtos
{
    public class HttpError
    {
        public List<TodoValidationException.ValidationError> ValidationErrors { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StackTrace { get; set; }
    }
}
