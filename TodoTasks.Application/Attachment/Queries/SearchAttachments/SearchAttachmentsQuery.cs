using MediatR;
using System.Collections.Generic;
using TodoTasks.Application.Attachment.Models;

namespace TodoTasks.Application.Attachment.Queries
{
    public class SearchAttachmentsQuery : IRequest<IEnumerable<AttachmentDto>>
    {
        public int? TodoId { get; set; }
    }
}
