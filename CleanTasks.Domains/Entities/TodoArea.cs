using System.Collections.Generic;

namespace CleanTasks.Domain.Entities
{
    public class TodoArea : AuditableEntity
    {
        public int TodoAreaId { get; set; }
        public string Name { get; set; }

        public ICollection<Todo> Todos { get; set; }
        public ICollection<TodoAreaPermission> Permissions { get; set; }
    }
}
