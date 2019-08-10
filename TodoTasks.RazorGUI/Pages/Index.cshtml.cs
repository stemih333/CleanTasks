using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TodoTasks.RazorGUI.Pages
{
    [AllowAnonymous]
    public class IndexModel : BaseModel
    {
        public string Username { get; set; }

        public void OnGet()
        {
            if(User.Identity.IsAuthenticated && User is ClaimsPrincipal)
            {
                Username = User.Identity.Name;
              
            }
        }
    }
}
