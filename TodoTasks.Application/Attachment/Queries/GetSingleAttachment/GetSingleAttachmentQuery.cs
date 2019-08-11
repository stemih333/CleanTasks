using MediatR;
using TodoTasks.Application.Attachment.Models;

namespace TodoTasks.Application.Attachment.Queries
{
    public class GetSingleAttachmentQuery : IRequest<AttachmentDto>
    {
        public int? AttachmentId { get; set; }
    }
}
