using CleanTasks.Application.TodoArea.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Interfaces
{
    public interface ITodoApiClient
    {
        Task<List<TodoAreaDto>> GetTodoAreas();
        Task<List<TodoAreaDto>> GetAllTodoAreas();
    }
}
