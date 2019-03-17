using CleanTasks.Application.TodoArea.Models;
using System.Collections.Generic;

namespace IdentityServer4.Quickstart.UI
{
    public class EditViewModel : ApplicationUserModel
    {
        public Dictionary<string, string> Permissions { get; set; }
        public List<TodoAreaDto> TodoAreas { get; set; }
    }
}
