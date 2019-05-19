using TodoTasks.Application.Exceptions;
using TodoTasks.Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.TodoArea.Commands
{
    public class UpdateTodoAreaHandler : IRequestHandler<UpdateTodoAreaCommand>
    {
        private readonly ITodoDbContext _todoDbContext;

        public UpdateTodoAreaHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(UpdateTodoAreaCommand request, CancellationToken cancellationToken)
        {
            var areaToUpdate = _todoDbContext.TodoAreas.FirstOrDefault(_ => _.TodoAreaId.Equals(request.TodoAreaId));

            if (areaToUpdate == null) throw new NotFoundException("Unable to find Todo area with ID: " + request.TodoAreaId.Value);

            areaToUpdate.Name = request.Name;

            _todoDbContext.TodoAreas.Update(areaToUpdate);

            await _todoDbContext.SaveAuditableChangesAsync(request.UserId, cancellationToken);

            return Unit.Value;
        }
    }
}
