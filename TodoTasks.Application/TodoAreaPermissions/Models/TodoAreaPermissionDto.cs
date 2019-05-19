namespace TodoTasks.Application.TodoAreaPermissions.Models
{
    public class TodoAreaPermissionDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int TodoAreaId { get; set; }
        public string AreaName { get; set; }
    }
}
