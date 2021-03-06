﻿using System.Collections.Generic;

namespace TodoTasks.Domain.Entities
{
    public class TodoArea : AuditableEntity
    {
        public int TodoAreaId { get; set; }
        public string Name { get; set; }

        public ICollection<Todo> Todos { get; set; }
    }
}
