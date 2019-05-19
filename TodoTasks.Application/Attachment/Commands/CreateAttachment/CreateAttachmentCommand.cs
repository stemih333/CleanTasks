using TodoTasks.Application.Attachment.Models;
using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentCommand : BaseCommand, IRequest<AttachmentDto>
    {
    }
}
