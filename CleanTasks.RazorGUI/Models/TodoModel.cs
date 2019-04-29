using CleanTasks.Domain.Enums;

public class TodoModel {
    public string UserName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string AssignedTo { get; set; }
    public string AssignedToName { get; set; }
    public int? Type { get; set; }
    public int? TodoAreaId { get; set; }
    public bool Notify { get; set; }
}