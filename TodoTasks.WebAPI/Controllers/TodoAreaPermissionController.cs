using System.Collections.Generic;
using System.Threading.Tasks;
using CleanTodoTasks.Application.TodoAreaPermissions.Commands;
using CleanTodoTasks.Application.TodoAreaPermissions.Models;
using CleanTodoTasks.Application.TodoAreaPermissions.Queries;
using CleanTodoTasks.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodoTasks.WebAPI.Controllers
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