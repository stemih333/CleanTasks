using CleanTasks.Application.ReferenceData.Models;
using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Application.TodoAreaPermissions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Interfaces
{
    public interface ITodoAreaApiClient
    {
        Task<List<TodoAreaPermissionDto>> GetAllPermissions();
        Task<List<TodoAreaPermissionDto>> GetPermissionsByUserId(string userId);
        Task<List<TodoAreaPermissionDto>> GetPermissionsByAreaId(int? areaId);
        Task<List<TodoAreaDto>> GetTodoAreas(List<string> allowedAreas);
        Task<List<TodoAreaDto>> GetAllTodoAreas();
        Task CreateTodoArea(string areaName, string userName);
        Task EditTodoArea(string areaName, int areaId, string userName);
        Task DeleteTodoArea(int areaId);
        Task<bool> AreaExist(string id);
        Task<ReferenceDataDto> GetReferenceData();
        Task CreateAreaPermission(int? areaId, string userId, string createdBy);
        Task DeleteAreaPermission(int? permissionId);
    }
}
