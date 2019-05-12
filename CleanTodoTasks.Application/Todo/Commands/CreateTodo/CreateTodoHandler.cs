using CleanTodoTasks.Common;
using CleanTodoTasks.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.Todo.Commands
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
                Description = request.Description,
                Status = TodoStatus.New,
                Title = request.Title,
                Type = request.Type.Value,
                Notify = request.Notify,
                TodoAreaId = request.TodoAreaId
            };

            await _todoDbContext.AddAsync(newTodo);

            await _todoDbContext.SaveAuditableChangesAsync(request.UserId);

            return newTodo.TodoId;
        }
    }
}
