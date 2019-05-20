using System.Linq;
using System.Security.Claims;

namespace TodoTasks.RazorGUI.Extensions
{
    public static class UserClaimExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal) => principal.Claims.FirstOrDefault(_ => _.Type.Equals("sub"))?.Value;
        public static string GetUserName(this ClaimsPrincipal principal) => principal.Claims.FirstOrDefault(_ => _.Type.Equals("name"))?.Value;
    }
}
