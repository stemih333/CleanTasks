using CleanTasks.Application.TodoTag.Commands;
using CleanTasks.WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CleanTasks.WebAPI.Controllers
{
    public class TodoTagController : TodoControllerBase
    {
        [HttpPut]
        public async Task<int> Put([FromBody][Required] IdValueInputModel model)
        => await Mediator.Send(new CreateTodoTagCommand { Value = model.Value, TodoId = model.Id });

        [HttpDelete("{id}")]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoTagCommand { TagId = id });
    }
}
