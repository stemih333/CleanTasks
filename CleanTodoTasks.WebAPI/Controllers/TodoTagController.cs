using CleanTodoTasks.Application.TodoTag.Commands;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CleanTodoTasks.WebAPI.Controllers
{
    public class TodoTagController : TodoControllerBase
    {
        [HttpPut]
        public async Task<int> Put([FromBody][Required] CreateTodoTagCommand model)
        => await Mediator.Send(model);

        [HttpDelete("{id}")]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoTagCommand { TagId = id });
    }
}
