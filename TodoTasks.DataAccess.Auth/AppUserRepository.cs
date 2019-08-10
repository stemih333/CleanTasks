using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoTasks.Application.Exceptions;
using TodoTasks.Application.Interfaces;
using TodoTasks.Domain.Entities;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.DataAccess.Auth
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppUserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<AppUser> GetUsers() => _userManager.Users.Select(GetAppUser);

        public async Task<IEnumerable<AppUser>> GetUsersByPermissionType(string permissionType)
            => (await _userManager.GetUsersForClaimAsync(new Claim(AuthConstants.PermissionType, permissionType)))?.Select(GetAppUser);

        public async Task<IEnumerable<AppUser>> SearchUsersByClaim(string claimType, string claimValue)
            => (await _userManager.GetUsersForClaimAsync(new Claim(claimType, claimValue)))?.Select(GetAppUser);

        public async Task<ClaimsIdentity> GetUserAsClaimsIdentity(string username)
        {
            var user = await _userManager.FindByEmailAsync(username) ?? await _userManager.FindByNameAsync(username);

            if (user == null) return null;
            IList<Claim> claimsToAdd = new List<Claim>();

            // Append the claims retrieved from database to user logged in with Open ID.
            if (!claimsToAdd.Any()) claimsToAdd = await _userManager.GetClaimsAsync(user);

            return new ClaimsIdentity(claimsToAdd);
        }

        public async Task CreateUserFromClaimsPrincipal(ClaimsPrincipal principal)
        {
            var name = principal.Claims.First(_ => _.Type.Equals(ClaimTypes.Name));
            var givenName = principal.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.GivenName));
            var surname = principal.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.Surname));
            if (name == null || givenName == null || surname == null) throw new AuthException($"User {principal.Identity.Name ?? "Unknown"} is missing one or more name claims.");

            var user = new ApplicationUser
            {
                Email = principal.Identity.Name,
                UserName = name.Value,
                FirstName = givenName.Value,
                LastName = surname.Value
            };

            var userResult = await _userManager.CreateAsync(user);
            if (!userResult.Succeeded) throw new AuthDbOperationException("Failed to create new user: " + principal.Identity.Name);

            var claimsToAdd = new List<Claim> { new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission) };

            var result = await _userManager.AddClaimsAsync(user, claimsToAdd);
            if (!result.Succeeded) throw new AuthDbOperationException("Failed to add claims for new user: " + principal.Identity.Name);
        }

        public async Task<PermissionUser> GetUser(string username)
        {
            var user = await GetApplicationUser(username);
            if (user == null) throw new NotFoundException($"User with username '{username}' does not exist.");
            var claims = await _userManager.GetClaimsAsync(user);
            if (claims == null) throw new NotFoundException($"User '{username}' does not have any claims assigned.");

            return new PermissionUser
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = user.Id,
                Permissions = claims.Select(_ => new AppPermission { PermissionName = _.Type, PermissionValue = _.Value })
            };
        }
        
        private AppUser GetAppUser(ApplicationUser user)
        => new AppUser
        {
            Email = user.Email,
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName
        };

        private async Task<ApplicationUser> GetApplicationUser(string username) => 
            await _userManager.FindByNameAsync(username) ??
            await _userManager.FindByEmailAsync(username) ??
            throw new NotFoundException($"User with username '{username}' does not exist.");

        public async Task SetUserPermission(string username, string permission)
        {
            var user = await GetApplicationUser(username);

            //Remove all permission
            await _userManager.RemoveClaimsAsync(user, new List<Claim>
            {
                new Claim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission),
                new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission)
            });

            var result = await _userManager.AddClaimAsync(user, new Claim(AuthConstants.PermissionType, permission));

            if (!result.Succeeded)
                throw new AuthDbOperationException($"Failed to add permission '{permission}' to user '{username}'.", result.Errors.Select(_ => _.Description));
        }

        public async Task SetAreaPermission(string username, string permission)
        {
            var user = await GetApplicationUser(username);

            var result = await _userManager.AddClaimAsync(user, new Claim(PermissionTypes.TodoAreaPermission, permission));

            if (!result.Succeeded)
                throw new AuthDbOperationException($"Failed to add area permission to user '{username}'.", result.Errors.Select(_ => _.Description));
        }

        public async Task RemoveAreaPermission(string username, string permission)
        {
            var user = await GetApplicationUser(username);

            var result = await _userManager.RemoveClaimAsync(user, new Claim(PermissionTypes.TodoAreaPermission, permission));

            if (!result.Succeeded)
                throw new AuthDbOperationException($"Failed to remove area permission to user '{username}'.", result.Errors.Select(_ => _.Description));
        }
    }
}
