namespace TodoTasks.Domain.Entities
{
    public class TodoComment : AuditableEntity
    {
        public int CommentId { get; set; }
        public string Value { get; set; }

        public int? TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}
