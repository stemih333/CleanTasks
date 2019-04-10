﻿using CleanTasks.Domain.Entities;
using CleanTasks.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.TodoAreaPermissions.Commands
{
    public class DeleteTodoAreaPermissionHandler : IRequestHandler<DeleteTodoAreaPermissionCommand>
    {
        private readonly TodoDbContext _todoDbContext;

        public DeleteTodoAreaPermissionHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(DeleteTodoAreaPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToDelete = new TodoAreaPermission { TodoAreaId = request.Id.Value };

            _todoDbContext.Remove(permissionToDelete);

            await _todoDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}