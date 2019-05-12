using CleanTodoTasks.RazorGUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTodoTasks.RazorGUI.Interfaces
{
    public interface IUserApiClient
    {
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetUsersByArea(int id);
    }
}
