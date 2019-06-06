using TodoTasks.Application.BaseClasses;
using MediatR;

namespace TodoTasks.Application.Attachment.Commands
{
    public class CreateAttachmentCommand : BaseCommand, IRequest<int>
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public int? TodoId { get; set; }
    }
}
