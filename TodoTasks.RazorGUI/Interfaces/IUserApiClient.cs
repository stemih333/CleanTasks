using TodoTasks.RazorGUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoTasks.RazorGUI.Interfaces
{
    public interface IUserApiClient
    {
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetUsersByArea(int id);
    }
}
