namespace TodoTasks.Domain.Entities
{
    public class Attachment : AuditableEntity
    {
        public int AttachmentId { get; set; }

        public string Name { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public string FileType { get; set; }
        public string SavedFileName { get; set; }

        public int? TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}
