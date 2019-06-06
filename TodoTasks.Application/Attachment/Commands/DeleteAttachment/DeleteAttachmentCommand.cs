using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.Attachment.Commands
{
    public class DeleteAttachmentCommand : IRequest
    {
        public int? AttachmentId { get; set; }
    }
}
