using System.Collections.Generic;

namespace TodoTasks.Domain.Entities
{
    public class PermissionUser : AppUser
    {
        public IEnumerable<AppPermission> Permissions { get; set; }
    }
}
