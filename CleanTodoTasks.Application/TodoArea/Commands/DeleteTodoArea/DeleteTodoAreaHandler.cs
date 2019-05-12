using CleanTodoTasks.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoArea.Commands
{
    public class DeleteTodoAreaHandler : IRequestHandler<DeleteTodoAreaCommand>
    {
        private readonly TodoDbContext _todoDbContext;

        public DeleteTodoAreaHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(DeleteTodoAreaCommand request, CancellationToken cancellationToken)
        {
            var areaToDelete = new Domain.Entities.TodoArea { TodoAreaId = request.TodoAreaId.Value };

            _todoDbContext.Remove(areaToDelete);

            await _todoDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
