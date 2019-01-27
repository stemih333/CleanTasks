using CleanTasks.Application.Attachment.Models;
using CleanTasks.Application.BaseClasses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanTasks.Application.Attachment.Commands.CreateAttachment
{
    public class CreateAttachmentCommand : BaseCommand, IRequest<AttachmentDto>
    {
        public IFormFile File { get; set; }
    }
}
