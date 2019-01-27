using CleanTasks.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.TodoArea.Commands
{
    public class CreateTodoAreaHandler : IRequestHandler<CreateTodoAreaCommand, int>
    {
        private readonly TodoDbContext _todoDbContext;

        public CreateTodoAreaHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<int> Handle(CreateTodoAreaCommand request, CancellationToken cancellationToken)
        {
            var newArea = new Domain.Entities.TodoArea
            {
                Name = request.Name
            };

            await _todoDbContext.AddAsync(newArea);

            await _todoDbContext.SaveAuditableChangesAsync(request.UserName, cancellationToken);

            return newArea.TodoAreaId;
        }

    }
}
