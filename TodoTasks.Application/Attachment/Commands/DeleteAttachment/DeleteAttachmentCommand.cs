using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.Attachment.Commands.DeleteAttachment
{
    public class DeleteAttachmentCommand : BaseCommand, IRequest
    {
        public int? TodoId { get; set; }
    }
}
