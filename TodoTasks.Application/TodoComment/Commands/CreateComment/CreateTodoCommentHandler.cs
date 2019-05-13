using CleanTodoTasks.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoComment.Commands
{
    public class CreateTodoCommentHandler : IRequestHandler<CreateTodoCommentCommand, int>
    {
        private readonly ITodoDbContext _todoDbContext;

        public CreateTodoCommentHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<int> Handle(CreateTodoCommentCommand request, CancellationToken cancellationToken)
        {
            var newComment = new Domain.Entities.TodoComment
            {
                TodoId = request.TodoId,
                Value = request.Value
            };

            await _todoDbContext.Comments.AddAsync(newComment);
            await _todoDbContext.SaveAuditableChangesAsync(request.UserId);

            return newComment.CommentId;
        }
    }
}
