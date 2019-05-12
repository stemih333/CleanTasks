using CleanTodoTasks.DataAccess;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoComment.Commands
{
    public class CreateTodoCommentHandler : IRequestHandler<CreateTodoCommentCommand, int>
    {
        private readonly TodoDbContext _todoDbContext;

        public CreateTodoCommentHandler(TodoDbContext todoDbContext)
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
