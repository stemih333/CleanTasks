using CleanTodoTasks.Application.ReferenceData.Models;
using System;

namespace CleanTodoTasks.Application.Todo.Models
{
    public class AuditedIdNameDto : IdNameDto
    {
        public string CreateBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
    }
}
