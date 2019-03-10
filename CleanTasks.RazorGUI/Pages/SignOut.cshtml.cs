using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages
{
    public class SignOutModel : PageModel
    {
        public async Task OnGet()
        {
            await HttpContext.SignOutAsync("Cookie");
            await HttpContext.SignOutAsync("oidc", new AuthenticationProperties { RedirectUri = "Https://localhost:5002/" });
        }
    }
}