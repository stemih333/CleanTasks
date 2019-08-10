using TodoTasks.Application.TodoArea.Models;
using MediatR;
using System.Collections.Generic;

namespace TodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaQuery : IRequest<List<TodoAreaDto>>
    {
        public IEnumerable<int> UserAreas { get; set; }
    }
}
