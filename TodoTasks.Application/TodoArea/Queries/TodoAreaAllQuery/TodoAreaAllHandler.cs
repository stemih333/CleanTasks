using TodoTasks.Application.Interfaces;
using TodoTasks.Application.TodoArea.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaAllHandler : IRequestHandler<TodoAreaAllQuery, List<TodoAreaDto>>
    {
        private readonly ITodoDbContext _todoDbContext;

        public TodoAreaAllHandler(ITodoDbContext todoDbContext)
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
