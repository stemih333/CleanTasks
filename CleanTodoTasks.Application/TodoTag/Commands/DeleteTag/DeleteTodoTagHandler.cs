using CleanTodoTasks.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoTag.Commands
{
    public class DeleteTodoTagHandler : IRequestHandler<DeleteTodoTagCommand>
    {
        private readonly TodoDbContext _todoDbContext;

        public DeleteTodoTagHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(DeleteTodoTagCommand request, CancellationToken cancellationToken)
        {
            var tagToDelete = new Domain.Entities.TodoTag { TagId = request.TagId.Value };

            _todoDbContext.Remove(tagToDelete);

            await _todoDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
