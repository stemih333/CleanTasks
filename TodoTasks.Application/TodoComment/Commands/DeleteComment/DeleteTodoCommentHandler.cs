using System.Threading;
using System.Threading.Tasks;
using CleanTodoTasks.Application.Interfaces;
using MediatR;

namespace CleanTodoTasks.Application.TodoComment.Commands
{
    public class DeleteTodoCommentHandler : IRequestHandler<DeleteTodoCommentCommand>
    {
        private readonly ITodoDbContext _todoDbContext;

        public DeleteTodoCommentHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(DeleteTodoCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToDelete = new Domain.Entities.TodoComment
            {
                CommentId = request.CommentId.Value
            };

            _todoDbContext.Comments.Remove(commentToDelete);

            await _todoDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
