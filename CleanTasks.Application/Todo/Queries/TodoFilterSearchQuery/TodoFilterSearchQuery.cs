using CleanTasks.Application.Todo.Models;
using MediatR;

namespace CleanTasks.Application.Todo.Queries
{
    public class TodoFilterSearchQuery : IRequest<PagedTodoResultDto>
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string SortPropery { get; set; }
        public string SortOrder { get; set; }
        public string FilterProperty { get; set; }
        public string FilterValue { get; set; }
        public string FilterOperator { get; set; }
        public int? TodoAreaId { get; set; }
    }
}
