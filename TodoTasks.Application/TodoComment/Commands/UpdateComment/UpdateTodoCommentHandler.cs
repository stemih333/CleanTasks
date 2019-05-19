using TodoTasks.Application.Exceptions;
using TodoTasks.Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.TodoComment.Commands
{
    public class UpdateTodoCommentHandler : IRequestHandler<UpdateTodoCommentCommand>
    {
        private readonly ITodoDbContext _todoDbContext;

        public UpdateTodoCommentHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Unit> Handle(UpdateTodoCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToUpdate = _todoDbContext.Comments.FirstOrDefault(_ => _.CommentId.Equals(request.CommentId));

            if (commentToUpdate == null) throw new NotFoundException($"Could not find comment with ID: " + request.CommentId);

            commentToUpdate.Value = request.Value;

            _todoDbContext.Comments.Update(commentToUpdate);

            await _todoDbContext.SaveAuditableChangesAsync(request.UserId);

            return Unit.Value;
        }
    }
}
