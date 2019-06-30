using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;

namespace TodoTasks.Application.Attachment.Commands
{
    public class CreateAttachmentHandler : IRequestHandler<CreateAttachmentCommand, int>
    {
        private readonly ITodoDbContext _todoDbContext;
        private readonly IFileSaver _fileSaver;
        private readonly string _filePath;

        public CreateAttachmentHandler(ITodoDbContext todoDbContext, IFileSaver fileSaver, IConfiguration configuration)
        {
            _todoDbContext = todoDbContext;
            _fileSaver = fileSaver;
            _filePath = configuration["FilePath"];
        }

        public async Task<int> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_filePath)) throw new Exception("Cannot create attachment. File path is missing in app settings.");
            var fullpath = Path.Combine(_filePath, Path.GetRandomFileName());
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
