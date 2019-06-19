using MediatR;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Attachment.Models;
using TodoTasks.Application.Exceptions;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.Attachment.Queries
{
    public class GetSingleAttachmentHandler : IRequestHandler<GetSingleAttachmentQuery, BinaryAttachmentDto>
    {
        private readonly ITodoDbContext _todoDbContext;

        public GetSingleAttachmentHandler(ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<BinaryAttachmentDto> Handle(GetSingleAttachmentQuery request, CancellationToken cancellationToken)
        {
            var file = _todoDbContext.Attachments.FirstOrDefault(_ => _.AttachmentId == request.AttachmentId);
            if (file == null) throw new NotFoundException("Could not find Attachment with id: " + request.AttachmentId);

            var fileBytes = await File.ReadAllBytesAsync(file.FilePath);

            return new BinaryAttachmentDto
            {
                AddedDate = file.Created,
                AttachmentId = file.AttachmentId,
                Description = file.Description,
                Name = file.Name,
                Size = file.Size,
                Type = file.FileType,
                TodoId = file.TodoId,
                FilePath = file.FilePath,
                FileBytes = fileBytes
            };
        }
    }
}
