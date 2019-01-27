namespace CleanTasks.Domain.Entities
{
    public class TodoTag : AuditableEntity
    {
        public int TagId { get; set; }
        public string Value { get; set; }

        public int? TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}
