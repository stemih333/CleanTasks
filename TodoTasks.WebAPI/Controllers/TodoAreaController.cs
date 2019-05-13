using System.Collections.Generic;
using System.Threading.Tasks;
using CleanTodoTasks.Application.TodoArea.Commands;
using CleanTodoTasks.Application.TodoArea.Models;
using CleanTodoTasks.Application.TodoArea.Queries;
using CleanTodoTasks.WebAPI.Filters;
using CleanTodoTasks.WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodoTasks.WebAPI.Controllers
{
    public class TodoAreaController : TodoControllerBase
    {
        [HttpGet, ValidateViewModel]
        public async Task<ActionResult<List<TodoAreaDto>>> Get([FromQuery]List<int> allowedAreas)
        {
            var areas = await Mediator.Send(new TodoAreaQuery { UserAreas = allowedAreas });
            return Ok(areas);
        }

        [HttpGet("all")]
        public async Task<List<TodoAreaDto>> GetAll() => await Mediator.Send(new TodoAreaAllQuery());

        [HttpPut, ValidateViewModel]
        public async Task<int> Put([FromBody] IdValueInputModel model)
        => await Mediator.Send(new CreateTodoAreaCommand { Name = model.Value, UserId = model.Username });
               
        [HttpPost, ValidateViewModel]
        public async Task Post([FromBody] IdValueInputModel model)
        => await Mediator.Send(new UpdateTodoAreaCommand { Name = model.Value, TodoAreaId = model.Id, UserId = model.Username });

        [HttpDelete("{id}")]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoAreaCommand { TodoAreaId = id });
    }
}
