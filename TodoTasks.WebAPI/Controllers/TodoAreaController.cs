﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TodoTasks.Application.TodoArea.Commands;
using TodoTasks.Application.TodoArea.Models;
using TodoTasks.Application.TodoArea.Queries;
using TodoTasks.WebAPI.Filters;
using TodoTasks.WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TodoTasks.WebAPI.Controllers
{
    public class TodoAreaController : TodoControllerBase
    {
        [HttpGet, ValidateViewModel]
        public async Task<ActionResult<List<TodoAreaDto>>> Get([FromQuery]List<int?> allowedAreas)
        => await Mediator.Send(new TodoAreaQuery { UserAreas = allowedAreas });

        [HttpPut, ValidateViewModel]
        public async Task<int> Put([FromBody] IdValueInputModel model)
        => await Mediator.Send(new CreateTodoAreaCommand { Name = model.Value, UserId = model.Username });
               
        [HttpPost, ValidateViewModel]
        public async Task Post([FromBody] IdValueInputModel model)
        => await Mediator.Send(new UpdateTodoAreaCommand { Name = model.Value, TodoAreaId = model.Id, UserId = model.Username });

        [HttpDelete("{id}")]
        public async Task Delete(int? id) => await Mediator.Send(new DeleteTodoAreaCommand { TodoAreaId = id });
    }
}
