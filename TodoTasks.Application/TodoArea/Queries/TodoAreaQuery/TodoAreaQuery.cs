using TodoTasks.Application.TodoArea.Models;
using MediatR;
using System.Collections.Generic;

namespace TodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaQuery : IRequest<List<TodoAreaDto>>
    {
        public List<int?> UserAreas { get; set; }
    }
}
