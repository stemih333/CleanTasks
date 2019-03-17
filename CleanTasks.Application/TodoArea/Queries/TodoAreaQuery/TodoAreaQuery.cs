using CleanTasks.Application.TodoArea.Models;
using MediatR;
using System.Collections.Generic;

namespace CleanTasks.Application.TodoArea.Queries
{
    public class TodoAreaQuery : IRequest<List<TodoAreaDto>>
    {
        public List<int> UserAreas { get; set; }
    }
}
