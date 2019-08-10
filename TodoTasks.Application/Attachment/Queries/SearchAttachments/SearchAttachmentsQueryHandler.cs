using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoTasks.Application.Attachment.Models;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.Attachment.Queries
{
    public class SearchAttachmentsQueryHandler : IRequestHandler<SearchAttachmentsQuery, IEnumerable<AttachmentDto>>
    {
        private readonly ITodoDbContext _todoDbContext;

        public SearchAttachmentsQueryHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<IEnumerable<AttachmentDto>> Handle(SearchAttachmentsQuery request, CancellationToken cancellationToken)
        {
            var attachments = _todoDbContext
                .Attachments
                .Where(_ => _.TodoId == request.TodoId)
                .Select(_ => new AttachmentDto
                {
                    Description = _.Description,
                    Name = _.Name,
                    Size = _.Size,
                    Type = _.FileType,
                    TodoId = _.TodoId,
                    AttachmentId = _.AttachmentId,
                    AddedDate = _.Created,
                    FilePath = _.SavedFileName
                }).ToList();

            return await Task.FromResult(attachments);
        }
    }
}
