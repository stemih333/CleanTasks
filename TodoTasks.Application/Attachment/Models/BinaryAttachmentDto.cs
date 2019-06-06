namespace TodoTasks.Application.Attachment.Models
{
    public class BinaryAttachmentDto : AttachmentDto
    {
        public byte[] FileBytes { get; set; }
    }
}
