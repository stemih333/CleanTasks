using CleanTasks.Application.Attachment.Models;
using CleanTasks.Application.BaseClasses;
using MediatR;

namespace CleanTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentCommand : BaseCommand, IRequest<AttachmentDto>
    {
    }
}
