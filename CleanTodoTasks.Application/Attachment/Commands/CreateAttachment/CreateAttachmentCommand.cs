using CleanTodoTasks.Application.Attachment.Models;
using CleanTodoTasks.Application.BaseClasses;
using MediatR;

namespace CleanTodoTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentCommand : BaseCommand, IRequest<AttachmentDto>
    {
    }
}
