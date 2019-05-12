using CleanTodoTasks.Application.TodoAreaPermissions.Models;
using CleanTodoTasks.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoAreaPermissions.Queries
{
    public class TodoAreaPermissionsHandler : IRequestHandler<TodoAreaPermissionsQuery, List<TodoAreaPermissionDto>>
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoAreaPermissionsHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public Task<List<TodoAreaPermissionDto>> Handle(TodoAreaPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = _todoDbContext
                .TodoAreaPermissions
                .Include(_ => _.TodoArea)
                .Select(_ => new TodoAreaPermissionDto
                {
                    AreaName = (_.TodoArea != null) ? _.TodoArea.Name : null,
                    Id = _.Id,
                    TodoAreaId = _.TodoAreaId.Value,
                    UserId = _.UserId
                });

            if (!string.IsNullOrEmpty(request.UserId)) permissions =  permissions.Where(_ => _.UserId.Equals(request.UserId));
            if (request.TodoAreaId.HasValue) permissions = permissions.Where(_ => _.TodoAreaId == request.TodoAreaId);

            return Task.FromResult(permissions.ToList());
        }
    }
}
