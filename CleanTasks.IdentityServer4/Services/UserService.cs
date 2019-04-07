using CleanTasks.Application.Interfaces;
using CleanTasks.Application.User.Models;
using CleanTasks.Common.Constants;
using CleanTasks.IdentityServer4.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanTasks.IdentityServer4.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserDto>> GetAllUsers()
            => (await _userManager.GetUsersForClaimAsync(new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission)))?
            .Select(MapFromApplicationUser)
            .ToList();

        public async Task<List<UserDto>> GetUsersByArea(int Id)
            => (await _userManager.GetUsersForClaimAsync(new Claim(PermissionTypes.TodoAreaPermission, Id.ToString())))?
            .Select(MapFromApplicationUser)
            .ToList();

        private UserDto MapFromApplicationUser(ApplicationUser user)
        => new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,           
            Email = user.Email,
            Id = user.Id,
            UserName = user.UserName
        };
    }
}
