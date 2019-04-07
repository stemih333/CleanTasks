using CleanTasks.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.Todo.Commands
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, int>
    {
        private readonly TodoDbContext _todoDbContext;

        public CreateTodoHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var newTodo = new Domain.Entities.Todo
            {
                AssignedTo = request.AssignedTo,
                AssignedToName = request.AssignedToName,
                Description = request.Description,
                Status = Domain.Enums.TodoStatuses.New,
                Title = request.Title,
                Type = request.Type,
            };

            await _todoDbContext.AddAsync(newTodo);

            await _todoDbContext.SaveAuditableChangesAsync(request.UserName);

            return newTodo.TodoId;
        }
    }
}
