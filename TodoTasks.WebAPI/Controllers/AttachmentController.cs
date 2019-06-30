using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoTasks.Application.Attachment.Commands;
using TodoTasks.Application.Attachment.Models;
using TodoTasks.Application.Attachment.Queries;
using TodoTasks.WebAPI.Models.InputModels;

namespace TodoTasks.WebAPI.Controllers
{
    public class AttachmentController : TodoControllerBase
    {
        [HttpPut]
        public async Task<int> Put([FromForm]AttachmentInputModel model)
        => await Mediator.Send(GetCommand(model));

        [HttpGet("{todoId:int?}")]
        public async Task<IEnumerable<AttachmentDto>> Get(int? todoId)
        => await Mediator.Send(new SearchAttachmentsQuery { TodoId = todoId });

        [HttpGet("single/{attachmentId:int?}")]
        public async Task<BinaryAttachmentDto> GetSingle(int? attachmentId)
        => await Mediator.Send(new GetSingleAttachmentQuery { AttachmentId = attachmentId });

        [HttpDelete("{attachmentId:int?}")]
        public async Task Delete(int? attachmentId)
        => await Mediator.Send(new DeleteAttachmentCommand { AttachmentId = attachmentId });

        private CreateAttachmentCommand GetCommand(AttachmentInputModel model)
        {
            if (model.File == null) return new CreateAttachmentCommand();
            using (var reader = new BinaryReader(model.File.OpenReadStream()))
            {
                return new CreateAttachmentCommand
                {                  
                    FileBytes = reader.ReadBytes((int)model.File.Length),
                    FileName = model.File.FileName,
                    UserId = model.UserId,
                    FileSize = model.File.Length,
                    FileType = model.File.ContentType,
                    Description = model.Description,
                    TodoId = model.TodoId
                };
            }
        }
    }
}