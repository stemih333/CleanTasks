using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Application.User.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Interfaces
{
    public interface ITodoApiClient
    {
        Task<List<TodoAreaDto>> GetTodoAreas(List<string> allowedAreas);
        Task<List<TodoAreaDto>> GetAllTodoAreas();
        Task CreateTodoArea(string areaName, string userName);
        Task DeleteTodoArea(int areaId);
    }
}
