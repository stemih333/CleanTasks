using CleanTasks.Domain.Enums;

namespace CleanTasks.WebAPI.Models.InputModels
{
    public class CreateTodoInputModel
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToName { get; set; }

        public TodoTypes Type { get; set; }

        public int? TodoAreaId { get; set; }
    }
}
