using CleanTasks.Application.Todo.Commands;
using CleanTasks.WebAPI.Filters;
using CleanTasks.WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanTasks.WebAPI.Controllers
{
    public class TodoController : TodoControllerBase
    {

        [HttpPut, ValidateViewModel]
        public async Task<int> Put([FromBody] CreateTodoInputModel model)
        => await Mediator.Send(new CreateTodoCommand {
            AssignedTo = model.AssignedTo,
            AssignedToName = model.AssignedToName,
            Description = model.Description,
            Title = model.Title,
            TodoAreaId = model.TodoAreaId,
            Type = model.Type,
            UserName = model.UserName
        });
    }
}
