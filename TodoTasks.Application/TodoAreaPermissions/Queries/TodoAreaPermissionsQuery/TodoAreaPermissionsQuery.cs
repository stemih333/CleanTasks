using TodoTasks.Application.TodoAreaPermissions.Models;
using MediatR;
using System.Collections.Generic;

namespace TodoTasks.Application.TodoAreaPermissions.Queries
{
    public class TodoAreaPermissionsQuery : IRequest<List<TodoAreaPermissionDto>>
    {
        public string UserId { get; set; }
        public int? TodoAreaId { get; set; }
    }
}
