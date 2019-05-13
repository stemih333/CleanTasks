using CleanTodoTasks.Application.Interfaces;
using CleanTodoTasks.Application.Todo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.Todo.Queries
{
    public class TodoFilterSearchQueryHandler : IRequestHandler<TodoFilterSearchQuery, PagedTodoResultDto>
    {
        private readonly ITodoDbContext _todoDbContext;

        public TodoFilterSearchQueryHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public Task<PagedTodoResultDto> Handle(TodoFilterSearchQuery request, CancellationToken cancellationToken)
        {
            var data = _todoDbContext.Todos.Include(_ => _.Comments).Include(_ => _.Tags).Where(_ => _.TodoAreaId == request.TodoAreaId).AsQueryable();

            if (!string.IsNullOrEmpty(request.FilterValue))
                data = data.Where($"{request.FilterProperty} {request.FilterOperator} {request.FilterValue}");

            var res = new PagedTodoResultDto
            {
                CurrentPage = request.CurrentPage.Value,
                PageSize = request.PageSize.Value,
                SortOrder = request.SortOrder,
                SortProperty = request.SortPropery,
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
                    TodoAreaName = _.TodoArea.Name,
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
