using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.Attachment.Commands.DeleteAttachment
{
    public class DeleteAttachmentCommand : BaseCommand, IRequest
    {
        public int? TodoId { get; set; }
    }
}
