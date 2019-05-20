using TodoTasks.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TodoTasks.Application.TodoArea.Commands
{
    public class DeleteTodoAreaHandler : IRequestHandler<DeleteTodoAreaCommand>
    {
        private readonly ITodoDbContext _todoDbContext;

        public DeleteTodoAreaHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(DeleteTodoAreaCommand request, CancellationToken cancellationToken)
        {
            var areaToDelete = new Domain.Entities.TodoArea { TodoAreaId = request.TodoAreaId.Value };
            var permissionsToDelete = _todoDbContext.TodoAreaPermissions.Where(_ => _.TodoAreaId == request.TodoAreaId.Value);

            _todoDbContext.TodoAreas.Remove(areaToDelete);

            await _todoDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
