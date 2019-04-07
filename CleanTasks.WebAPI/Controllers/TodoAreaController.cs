using System.Collections.Generic;
using System.Threading.Tasks;
using CleanTasks.Application.TodoArea.Commands;
using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Application.TodoArea.Queries;
using CleanTasks.WebAPI.Filters;
using CleanTasks.WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanTasks.WebAPI.Controllers
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
        => await Mediator.Send(new CreateTodoAreaCommand { Name = model.Value, UserName = model.Username });
               
        [HttpPost, ValidateViewModel]
        public async Task Post([FromBody] IdValueInputModel model)
        => await Mediator.Send(new UpdateTodoAreaCommand { Name = model.Value, TodoAreaId = model.Id, UserName = model.Username });

        [HttpDelete("{id}")]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoAreaCommand { TodoAreaId = id });
    }
}
