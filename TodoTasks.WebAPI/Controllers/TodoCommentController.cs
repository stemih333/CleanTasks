using CleanTodoTasks.Application.TodoComment.Commands;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CleanTodoTasks.WebAPI.Controllers
{
    public class TodoCommentController : TodoControllerBase
    {
        [HttpPut]
        public async Task<int> Put([FromBody][Required] CreateTodoCommentCommand model)
        => await Mediator.Send(model);

        [HttpPost]
        public async Task Post([FromBody][Required] CreateTodoCommentCommand model)
        => await Mediator.Send(model);

        [HttpDelete("{id}")]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoCommentCommand { CommentId = id });
    }
}
