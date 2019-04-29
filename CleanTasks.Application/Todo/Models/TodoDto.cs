using CleanTasks.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CleanTasks.Application.Todo.Models
{
    public class TodoDto
    {
        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public bool? Notify { get; set; }

        public IEnumerable<AuditedIdNameDto> Comments { get; set; }
        public IEnumerable<AuditedIdNameDto> Tags { get; set; }

        public TodoTypes? Type { get; set; }
        public TodoStatuses? Status { get; set; }
        public TodoReasons? CloseReason { get; set; }

        public int? LinkedTodoId { get; set; }
        public int LinkedTodoTitle { get; set; }

        public byte[] Rowversion { get; set; }

        public int? TodoAreaId { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
