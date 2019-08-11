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
    public class TodoAreaHandler : IRequestHandler<TodoAreaQuery, List<TodoAreaDto>>
    {
        private readonly ITodoDbContext _todoDbContext;

        public TodoAreaHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public Task<List<TodoAreaDto>> Handle(TodoAreaQuery request, CancellationToken cancellationToken)
        {
            var areas = _todoDbContext.TodoAreas
                .Include(_ => _.Todos)
                .Select(_ => new TodoAreaDto
                {
                    Name = _.Name,
                    TodoAreaId = _.TodoAreaId,
                    TodoCount = _.Todos.Count()
                });

            if (request.UserAreas != null) areas = areas.Where(_ => request.UserAreas.Contains(_.TodoAreaId));

            return Task.FromResult(areas.ToList());          
        }
    }
}
