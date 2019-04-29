using CleanTasks.Application.Todo.Commands;
using CleanTasks.Application.Todo.Models;
using CleanTasks.Application.Todo.Queries;
using CleanTasks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTasks.WebAPI.Controllers
{
    public class TodoController : TodoControllerBase
    {
        [HttpPut, ValidateViewModel]
        public async Task<int> Put([FromBody] CreateTodoCommand model)
        => await Mediator.Send(model);

        [HttpGet, ValidateViewModel]
        public async Task<PagedTodoResultDto> Get([FromQuery] TodoSearchQuery model)
        => await Mediator.Send(model);

        [HttpGet("filter"), ValidateViewModel]
        public async Task<PagedTodoResultDto> Filter([FromQuery] TodoFilterSearchQuery model)
       => await Mediator.Send(model);
    }
}
