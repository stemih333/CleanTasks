using System;

namespace TodoTasks.Application.Attachment.Models
{
    public class AttachmentDto
    {
        public int AttachmentId { get; set; }

        public int? TodoId { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public string FilePath { get; set; }
    }
}
