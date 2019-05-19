using TodoTasks.Application.Todo.Commands;
using TodoTasks.Application.Todo.Models;
using TodoTasks.Application.Todo.Queries;
using TodoTasks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TodoTasks.WebAPI.Controllers
{
    public class TodoController : TodoControllerBase
    {
        [HttpPut, ValidateViewModel]
        public async Task<int> Put([FromBody] CreateTodoCommand model)
        => await Mediator.Send(model);

        [HttpPost, ValidateViewModel]
        public async Task<int> Post([FromBody] EditTodoCommand model)
        => await Mediator.Send(model);

        [HttpGet, ValidateViewModel]
        public async Task<PagedTodoResultDto> Get([FromQuery] TodoSearchQuery model)
        => await Mediator.Send(model);

        [HttpGet("filter"), ValidateViewModel]
        public async Task<PagedTodoResultDto> Filter([FromQuery] TodoFilterSearchQuery model)
       => await Mediator.Send(model);
    }
}
