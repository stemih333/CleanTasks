using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.Attachment.Commands.DeleteAttachment
{
    public class DeleteAttachmentCommand : BaseCommand, IRequest
    {
        public int? TodoId { get; set; }
    }
}
