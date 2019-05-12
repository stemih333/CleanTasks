using CleanTodoTasks.Application.Exceptions;
using CleanTodoTasks.DataAccess;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoArea.Commands
{
    public class UpdateTodoAreaHandler : IRequestHandler<UpdateTodoAreaCommand>
    {
        private readonly TodoDbContext _todoDbContext;

        public UpdateTodoAreaHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(UpdateTodoAreaCommand request, CancellationToken cancellationToken)
        {
            var areaToUpdate = _todoDbContext.TodoAreas.FirstOrDefault(_ => _.TodoAreaId.Equals(request.TodoAreaId));

            if (areaToUpdate == null) throw new NotFoundException("Unable to find Todo area with ID: " + request.TodoAreaId.Value);

            areaToUpdate.Name = request.Name;

            _todoDbContext.Update(areaToUpdate);

            await _todoDbContext.SaveAuditableChangesAsync(request.UserId, cancellationToken);

            return Unit.Value;
        }
    }
}
