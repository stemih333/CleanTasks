using CleanTasks.Application.Todo.Models;
using CleanTasks.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.Todo.Queries
{
    public class TodoSearchQueryHandler : IRequestHandler<TodoSearchQuery, PagedTodoResultDto>
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoSearchQueryHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public Task<PagedTodoResultDto> Handle(TodoSearchQuery request, CancellationToken cancellationToken)
        {
            var data = _todoDbContext.Todos.Include(_ => _.Comments).Include(_ => _.Tags).AsQueryable();

            if (request.TodoId.HasValue) data = data.Where(_ => _.TodoId == request.TodoId);
            if (request.TodoAreaId.HasValue) data = data.Where(_ => _.TodoAreaId == request.TodoAreaId);
            if (!string.IsNullOrEmpty(request.AssignedTo)) data = data.Where(_ => _.AssignedTo.Equals(request.AssignedTo));
            if (!string.IsNullOrEmpty(request.Description)) data.Where(_ => _.Description.Contains(request.Description));
            if (!string.IsNullOrEmpty(request.Title)) data.Where(_ => _.Title.Contains(request.Title));
            if (!string.IsNullOrEmpty(request.Comment)) data.Where(_ => _.Comments.Any(x => x.Value.Contains(request.Comment)));
            if (!string.IsNullOrEmpty(request.Tags)) data.Where(_ => _.Tags.Any(x => x.Value.Contains(request.Tags)));
            if (!string.IsNullOrEmpty(request.UpdatedBy)) data = data.Where(_ => _.UpdatedBy.Equals(request.UpdatedBy));
            if (!string.IsNullOrEmpty(request.CreatedBy)) data = data.Where(_ => _.CreatedBy.Equals(request.CreatedBy));
            if (request.Notify.HasValue) data.Where(_ => _.Notify == request.Notify.Value);
            if (request.Created.HasValue) data.Where(_ => _.Created == request.Created);
            if (request.Updated.HasValue) data.Where(_ => _.Updated == request.Updated);
            if (request.Type.HasValue) data.Where(_ => _.Type == request.Type);
            if (request.Status.HasValue) data.Where(_ => _.Status == request.Status);
            if (request.CloseReason.HasValue) data.Where(_ => _.CloseReason == request.CloseReason);

            var res = new PagedTodoResultDto
            {
                CurrentPage = request.CurrentPage.Value,
                PageSize = request.PageSize.Value,
                SortOrder = request.SortOrder,
                SortProperty = request.SortProperty,
                Todos = data.Select(_ => new TodoDto
                {
                    AssignedTo = _.AssignedTo,
                    CloseReason = _.CloseReason,
                    Comments = (_.Comments != null) ? _.Comments.Select(x => new AuditedIdNameDto
                    {
                        Id = x.CommentId,
                        CreateBy = x.CreatedBy,
                        Created = x.Created,
                        Name = x.Value,
                        Updated = x.Updated,
                        UpdatedBy = x.UpdatedBy
                    }) : null,
                    Tags = (_.Tags != null) ? _.Tags.Select(x => new AuditedIdNameDto
                    {
                        Id = x.TagId,
                        CreateBy = x.CreatedBy,
                        Created = x.Created,
                        Name = x.Value,
                        Updated = x.Updated,
                        UpdatedBy = x.UpdatedBy
                    }) : null,
                    Description = _.Description,
                    Notify = _.Notify,
                    Status = _.Status,
                    Title = _.Title,
                    TodoAreaId = _.TodoAreaId,
                    TodoId = _.TodoId,
                    Type = _.Type,
                    Created = _.Created,
                    CreatedBy = _.CreatedBy,
                    Updated = _.Updated,
                    UpdatedBy = _.UpdatedBy
                })
                .Skip(request.PageSize.Value * (request.CurrentPage.Value - 1))
                .Take(request.PageSize.Value)
                .ToList()
            };

            return Task.FromResult(res);
        }
    }
}
