namespace CleanTodoTasks.Application.TodoTag.Models
{
    public class TagDto
    {
        public int TagId { get; set; }
        public string Value { get; set; }
        public int? TodoId { get; set; }
    }
}
