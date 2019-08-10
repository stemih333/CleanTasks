using System.Collections.Generic;
using System.Security.Claims;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.DataAccess.Auth
{
    public static class Users
    {
        public static ApplicationUser Admin = new ApplicationUser
        {
            Email = "stefan.mihailovic@if.se",
            UserName = "stefan.mihailovic@if.se",
            FirstName = "Stefan",
            LastName = "Mihailovic"
        };

        public static List<Claim> AdminClaims = new List<Claim>
        {
            new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission),
            new Claim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission),
            new Claim(PermissionTypes.TodoAreaPermission, "1"),
            new Claim(PermissionTypes.TodoAreaPermission, "2"),
        };
    }
}
