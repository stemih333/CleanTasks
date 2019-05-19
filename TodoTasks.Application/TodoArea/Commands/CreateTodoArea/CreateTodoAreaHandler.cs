using TodoTasks.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.TodoArea.Commands
{
    public class CreateTodoAreaHandler : IRequestHandler<CreateTodoAreaCommand, int>
    {
        private readonly ITodoDbContext _todoDbContext;

        public CreateTodoAreaHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<int> Handle(CreateTodoAreaCommand request, CancellationToken cancellationToken)
        {
            var newArea = new Domain.Entities.TodoArea
            {
                Name = request.Name
            };

            await _todoDbContext.TodoAreas.AddAsync(newArea);

            await _todoDbContext.SaveAuditableChangesAsync(request.UserId, cancellationToken);

            return newArea.TodoAreaId;
        }
    }
}
