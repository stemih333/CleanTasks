namespace CleanTodoTasks.Domain.Entities
{
    public class Attachment : AuditableEntity
    {
        public int AttachmentId { get; set; }

        public string Name { get; set; }
        public int Size { get; set; }

        public int? TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}
