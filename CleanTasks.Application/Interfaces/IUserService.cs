using CleanTasks.Application.User.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTasks.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetUsersByArea(int Id);
    }
}
