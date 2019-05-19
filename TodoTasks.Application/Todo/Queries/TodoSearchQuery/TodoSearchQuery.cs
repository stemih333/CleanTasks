using TodoTasks.Application.Todo.Models;
using TodoTasks.Common;
using MediatR;
using System;

namespace TodoTasks.Application.Todo.Queries
{
    public class TodoSearchQuery : IRequest<PagedTodoResultDto>
    {
        public int? TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public bool? Notify { get; set; }
        public string Comment { get; set; }
        public string Tags { get; set; }
        public TodoType? Type { get; set; }
        public TodoStatus? Status { get; set; }
        public TodoReason? CloseReason { get; set; }
        public int? TodoAreaId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public string SortOrder { get; set; }
        public string SortProperty { get; set; }
    }
}
