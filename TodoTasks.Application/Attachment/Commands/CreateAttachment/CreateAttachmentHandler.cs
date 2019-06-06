using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;
using System.IO;
using System.Collections.Generic;

namespace TodoTasks.Application.Attachment.Commands
{
    public class CreateAttachmentHandler : IRequestHandler<CreateAttachmentCommand, int>
    {
        private readonly ITodoDbContext _todoDbContext;
        private readonly IFileSaver _fileSaver;

        public CreateAttachmentHandler(ITodoDbContext todoDbContext, IFileSaver fileSaver)
        {
            _todoDbContext = todoDbContext;
            _fileSaver = fileSaver;
        }

        public async Task<int> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            var fullpath = Path.Combine(request.FilePath, Path.GetRandomFileName());
            await _fileSaver.SaveFile(fullpath, request.FileBytes);

            _todoDbContext.Attachments.Add(new Domain.Entities.Attachment
            {
                TodoId = request.TodoId,
                Description = request.Description,
                FilePath = fullpath,
                FileType = request.FileType,
                Name = request.FileName,
                Size = request.FileSize               
            });

            return await _todoDbContext.SaveAuditableChangesAsync(request.UserId);
        }
    }
}
