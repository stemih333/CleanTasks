using System.Collections.Generic;
using System.Threading.Tasks;
using TodoTasks.Domain.Entities;

namespace TodoTasks.Application.Interfaces
{
    public interface IAppUserRepository
    {
        Task SetUserPermission(string username, string permission);
        Task SetAreaPermission(string username, string permission);
        Task RemoveAreaPermission(string username, string permission);
        IEnumerable<Domain.Entities.AppUser> GetUsers();
        Task<IEnumerable<Domain.Entities.AppUser>> GetUsersByPermissionType(string permissionType);
        Task<PermissionUser> GetUser(string username);
        Task<IEnumerable<Domain.Entities.AppUser>> SearchUsersByClaim(string claimType, string claimValue);
    }
}
