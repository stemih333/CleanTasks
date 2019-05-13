namespace CleanTodoTasks.Application.Attachment.Models
{
    public class AttachmentDto
    {
        public int? TodoId { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set; }
        public string FileType { get; set; }
    }
}
