using CleanTodoTasks.Application.TodoArea.Models;
using CleanTodoTasks.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTodoTasks.Application.TodoArea.Queries
{
    public class TodoAreaHandler : IRequestHandler<TodoAreaQuery, List<TodoAreaDto>>
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoAreaHandler(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public Task<List<TodoAreaDto>> Handle(TodoAreaQuery request, CancellationToken cancellationToken)
        {           
            var areas = _todoDbContext.TodoAreas
                .Include(_ => _.Todos)
                .Where(_ => request.UserAreas.Contains(_.TodoAreaId))
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
