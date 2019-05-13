using CleanTodoTasks.Common;
using System.Collections.Generic;

namespace CleanTodoTasks.Domain.Entities
{
    public class Todo : AuditableEntity
    {
        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public bool? Notify { get; set; }

        public ICollection<TodoComment> Comments { get; set; } = new List<TodoComment>();
        public ICollection<TodoTag> Tags { get; set; } = new List<TodoTag>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
       
        public TodoType Type { get; set; }
        public TodoStatus Status { get; set; }
        public TodoReason? CloseReason { get; set; }

        public int? LinkedTodoId { get; set; }
        public Todo LinkedTodo { get; set; }
        public ICollection<Todo> LinkedTodos { get; set; } = new List<Todo>();

        public byte[] Rowversion { get; set; }

        public int? TodoAreaId { get; set; }
        public TodoArea TodoArea { get; set; }
    }
}
