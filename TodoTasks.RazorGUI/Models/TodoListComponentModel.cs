using TodoTasks.Application.Todo.Models;
using System.Collections.Generic;

namespace TodoTasks.RazorGUI.Models
{
    public class TodoListComponentModel
    {
        public int TodoAreaId { get; set; }
        public string TodoAreaName { get; set; }
        public List<TodoDto> Todos { get; set; }
    }
}
