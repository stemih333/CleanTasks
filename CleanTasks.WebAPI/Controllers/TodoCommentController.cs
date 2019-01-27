using CleanTasks.Application.TodoComment.Commands;
using CleanTasks.WebAPI.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CleanTasks.WebAPI.Controllers
{
    public class TodoCommentController : TodoControllerBase
    {
        [HttpPut]
        public async Task<int> Put([FromBody][Required] IdValueInputModel model)
        => await Mediator.Send(new CreateTodoCommentCommand { Value = model.Value, TodoId = model.Id });

        [HttpPost]
        public async Task Post([FromBody][Required] IdValueInputModel model)
        => await Mediator.Send(new UpdateTodoCommentCommand { Value = model.Value, CommentId = model.Id });

        [HttpDelete("{id}")]
        public async Task Delete(int? id)
        => await Mediator.Send(new DeleteTodoCommentCommand { CommentId = id });
    }
}
