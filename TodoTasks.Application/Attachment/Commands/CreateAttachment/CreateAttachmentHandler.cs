using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace TodoTasks.Application.Attachment.Commands
{
    public class CreateAttachmentHandler : IRequestHandler<CreateAttachmentCommand, int>
    {
        private readonly ITodoDbContext _todoDbContext;
        private readonly IFileSaver _fileSaver;

        public CreateAttachmentHandler(ITodoDbContext todoDbContext, IFileSaver fileSaver, IConfiguration configuration)
        {
            _todoDbContext = todoDbContext;
            _fileSaver = fileSaver;
        }

        public async Task<int> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
        {
            var randomFileName = Path.GetRandomFileName();
            await _fileSaver.SaveFile(randomFileName, request.FileStream);

            _todoDbContext.Attachments.Add(new Domain.Entities.Attachment
            {
                TodoId = request.TodoId,
                Description = request.Description,
                FileType = request.FileType,
                Name = request.FileName,
                Size = request.FileSize,
                SavedFileName = randomFileName
            });

            return await _todoDbContext.SaveAuditableChangesAsync(request.UserId);
        }
    }
}
