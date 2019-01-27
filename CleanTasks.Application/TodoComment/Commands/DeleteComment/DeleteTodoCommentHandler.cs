using System.Threading;
using System.Threading.Tasks;
using CleanTasks.Persistence;
using MediatR;

namespace CleanTasks.Application.TodoComment.Commands
{
    public class DeleteTodoCommentHandler : IRequestHandler<DeleteTodoCommentCommand>
    {
        private readonly TodoDbContext _todoDbContext;

        public DeleteTodoCommentHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(DeleteTodoCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToDelete = new Domain.Entities.TodoComment
            {
                CommentId = request.CommentId.Value
            };

            _todoDbContext.Remove(commentToDelete);

            await _todoDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
