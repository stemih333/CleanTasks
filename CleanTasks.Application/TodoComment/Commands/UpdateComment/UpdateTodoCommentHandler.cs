using CleanTasks.Application.Exceptions;
using CleanTasks.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.TodoComment.Commands
{
    public class UpdateTodoCommentHandler : IRequestHandler<UpdateTodoCommentCommand>
    {
        private readonly TodoDbContext _todoDbContext;

        public UpdateTodoCommentHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(UpdateTodoCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToUpdate = _todoDbContext.Comments.FirstOrDefault(_ => _.CommentId.Equals(request.CommentId));

            if (commentToUpdate == null) throw new NotFoundException($"Could not find comment with ID: " + request.CommentId);

            commentToUpdate.Value = request.Value;

            _todoDbContext.Update(commentToUpdate);

            await _todoDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
