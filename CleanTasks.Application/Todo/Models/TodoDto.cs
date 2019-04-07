using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Domain.Enums;
using System.Collections.Generic;

namespace CleanTasks.Application.Todo.Models
{
    public class TodoDto
    {
        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToName { get; set; }

        public ICollection<AuditedIdNameDto> Comments { get; set; }
        public ICollection<AuditedIdNameDto> Tags { get; set; }

        public TodoTypes Type { get; set; }
        public TodoStatuses Status { get; set; }
        public TodoReasons CloseReason { get; set; }

        public int? LinkedTodoId { get; set; }
        public int LinkedTodoTitle { get; set; }

        public byte[] Rowversion { get; set; }

        public TodoAreaDto MyProperty { get; set; }
    }
}
