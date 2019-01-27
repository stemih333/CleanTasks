using CleanTasks.Domain.Enums;
using System.Collections.Generic;

namespace CleanTasks.Domain.Entities
{
    public class Todo : AuditableEntity
    {
        public int TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }

        public ICollection<TodoComment> Comments { get; set; } = new List<TodoComment>();
        public ICollection<TodoTag> Tags { get; set; } = new List<TodoTag>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
       
        public TodoTypes Type { get; set; }
        public TodoStatuses Status { get; set; }
        public TodoReasons CloseReason { get; set; }

        public int? LinkedTodoId { get; set; }
        public Todo LinkedTodo { get; set; }
        public ICollection<Todo> LinkedTodos { get; set; } = new List<Todo>();

        public byte[] Rowversion { get; set; }
    }
}
