using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanTasks.Application.TodoArea.Commands;
using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Application.TodoArea.Queries;
using CleanTasks.Common.Constants;
using CleanTasks.WebAPI.Filters;
using CleanTasks.WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CleanTasks.WebAPI.Controllers
{
    public class TodoAreaController : TodoControllerBase
    {
        readonly ILogger log;

        public TodoAreaController(ILogger log)
        {
            this.log = log;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoAreaDto>>> Get()
        {
            var allowedAreas = User.Claims
                .Where(_ => _.Type.Equals(PermissionTypes.TodoAreaPermission))
                .Select(_ => int.Parse(_.Value))
                .ToList();

            await Mediator.Send(new TodoAreaQuery { UserAreas = allowedAreas });
            return Ok();
        }

        [HttpPut, ValidateViewModel]
        public async Task<int> Put([FromBody] IdValueInputModel model)
        => await Mediator.Send(new CreateTodoAreaCommand { Name = model.Value, UserName = "TestCreate" });
               
        [HttpPost, ValidateViewModel]
        public async Task Post([FromBody] IdValueInputModel model)
        => await Mediator.Send(new UpdateTodoAreaCommand { Name = model.Value, TodoAreaId = model.Id, UserName = "TestUpdate" });

        [HttpDelete("{id}")]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoAreaCommand { TodoAreaId = id });
    }
}
