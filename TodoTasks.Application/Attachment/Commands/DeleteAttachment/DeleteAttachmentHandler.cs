using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoTasks.Application.Exceptions;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.Attachment.Commands
{
    public class DeleteAttachmentHandler : IRequestHandler<DeleteAttachmentCommand>
    {
        private readonly ITodoDbContext _todoDbContext;
        private readonly IFileSaver _fileSaver;

        public DeleteAttachmentHandler(ITodoDbContext todoDbContext, IFileSaver fileSaver)
        {
            _todoDbContext = todoDbContext;
            _fileSaver = fileSaver;
        }

        public async Task<Unit> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            var file = _todoDbContext.Attachments.FirstOrDefault(_ => _.AttachmentId == request.AttachmentId);
            if (file == null) throw new NotFoundException("Could not find Attachment with id: " + request.AttachmentId);

            _todoDbContext.Attachments.Remove(file);

            await _todoDbContext.SaveChangesAsync();

            await _fileSaver.DeleteFile(file.FilePath);

            return await Unit.Task;
        }
    }
}
