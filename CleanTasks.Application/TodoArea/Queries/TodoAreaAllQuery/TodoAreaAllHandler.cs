using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.TodoArea.Queries
{
    public class TodoAreaAllHandler : IRequestHandler<TodoAreaAllQuery, List<TodoAreaDto>>
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoAreaAllHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public Task<List<TodoAreaDto>> Handle(TodoAreaAllQuery request, CancellationToken cancellationToken)
        {           
            var areas = _todoDbContext.TodoAreas
                .Include(_ => _.Todos)
                .Select(_ => new TodoAreaDto
                {
                    Name = _.Name,
                    TodoAreaId = _.TodoAreaId,
                    TodoCount = _.Todos.Count()
                })
                .ToList();

            return Task.FromResult(areas);          
        }
    }
}
