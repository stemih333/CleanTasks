using CleanTodoTasks.Application.Todo.Commands;
using CleanTodoTasks.Application.Todo.Models;
using CleanTodoTasks.Application.Todo.Queries;
using CleanTodoTasks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanTodoTasks.WebAPI.Controllers
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
