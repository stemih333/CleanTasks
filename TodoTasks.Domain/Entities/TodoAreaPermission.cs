namespace CleanTodoTasks.Domain.Entities
{
    public class TodoAreaPermission : AuditableEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int? TodoAreaId { get; set; }
        public TodoArea TodoArea { get; set; }
    }
}
