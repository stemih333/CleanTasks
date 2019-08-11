using System;
using System.Collections.Generic;
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
        {
            if (model.File == null) throw new ArgumentNullException("No file has been sent to server.");
            using (var stream = model.File.OpenReadStream())
            {
                return await Mediator.Send(
                new CreateAttachmentCommand
                {
                    FileStream = stream,
                    FileName = model.File.FileName,
                    UserId = model.UserId,
                    FileSize = model.File.Length,
                    FileType = model.File.ContentType,
                    Description = model.Description,
                    TodoId = model.TodoId
                });
            }
        }

        [HttpGet("{todoId:int?}")]
        public async Task<IEnumerable<AttachmentDto>> Get(int? todoId)
        => await Mediator.Send(new SearchAttachmentsQuery { TodoId = todoId });

        [HttpGet("single/{attachmentId:int?}")]
        public async Task<AttachmentDto> GetSingle(int? attachmentId)
        => await Mediator.Send(new GetSingleAttachmentQuery { AttachmentId = attachmentId });

        [HttpDelete("{attachmentId:int?}")]
        public async Task Delete(int? attachmentId)
        => await Mediator.Send(new DeleteAttachmentCommand { AttachmentId = attachmentId });
    }
}