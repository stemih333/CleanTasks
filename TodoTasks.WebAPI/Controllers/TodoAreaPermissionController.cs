using System.Collections.Generic;
using System.Threading.Tasks;
using TodoTasks.Application.TodoAreaPermissions.Commands;
using TodoTasks.Application.TodoAreaPermissions.Models;
using TodoTasks.Application.TodoAreaPermissions.Queries;
using TodoTasks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace TodoTasks.WebAPI.Controllers
{
    public class TodoAreaPermissionController : TodoControllerBase
    {
        [HttpGet, ValidateViewModel]
        public async Task<ActionResult<List<TodoAreaPermissionDto>>> Get([FromQuery]TodoAreaPermissionsQuery model)
        => await Mediator.Send(model);

        [HttpPut, ValidateViewModel]
        public async Task<int> Put([FromBody]CreateTodoAreaPermissionCommand model)
        => await Mediator.Send(model);

        [HttpDelete("{id:int?}"), ValidateViewModel]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoAreaPermissionCommand { Id = id });
    }
}