using TodoTasks.Application.ReferenceData.Models;
using TodoTasks.Application.TodoArea.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoTasks.RazorGUI.Interfaces
{
    public interface ITodoAreaApiClient
    {
        Task<List<TodoAreaDto>> GetTodoAreas(IEnumerable<string> allowedAreas);
        Task<List<TodoAreaDto>> GetAllTodoAreas();
        Task CreateTodoArea(string areaName, string userName);
        Task EditTodoArea(string areaName, int areaId, string userName);
        Task DeleteTodoArea(int areaId);
        Task<bool> AreaExist(string id);
        Task<ReferenceDataDto> GetReferenceData();
        Task CreateAreaPermission(int? areaId, string username);
        Task DeleteAreaPermission(int? areaId, string username);
    }
}
