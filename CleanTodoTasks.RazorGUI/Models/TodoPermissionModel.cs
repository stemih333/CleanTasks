using System.Collections.Generic;

namespace CleanTodoTasks.RazorGUI.Models
{
    public class TodoPermissionModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public List<string> PermittedAreas { get; set; }
    }
}
