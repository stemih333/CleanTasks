using CleanTodoTasks.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoTag.Commands
{
    public class CreateTodoTagHandler : IRequestHandler<CreateTodoTagCommand, int>
    {
        private readonly ITodoDbContext _todoDbContext;

        public CreateTodoTagHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<int> Handle(CreateTodoTagCommand request, CancellationToken cancellationToken)
        {
            var newTag = new Domain.Entities.TodoTag
            {
                TodoId = request.TodoId,
                Value = request.Value
            };

            await _todoDbContext.Tags.AddAsync(newTag);
            await _todoDbContext.SaveAuditableChangesAsync(request.UserId);

            return newTag.TagId;
        }
    }
}
