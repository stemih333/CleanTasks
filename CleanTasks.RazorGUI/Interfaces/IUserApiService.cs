using CleanTasks.Application.User.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Interfaces
{
    public interface IUserApiService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetUsersByArea(int id);
    }
}
