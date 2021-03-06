﻿using System;

namespace TodoTasks.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
