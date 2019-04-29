using System.Collections.Generic;
using System.Threading.Tasks;
using CleanTasks.Application.TodoAreaPermissions.Commands;
using CleanTasks.Application.TodoAreaPermissions.Models;
using CleanTasks.Application.TodoAreaPermissions.Queries;
using CleanTasks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanTasks.WebAPI.Controllers
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