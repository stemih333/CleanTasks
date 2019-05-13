using CleanTodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CleanTodoTasks.RazorGUI.Pages
{
    public class SignOutModel : PageModel
    {
        private readonly IAppSessionHandler _appSessionHandler;

        public SignOutModel(IAppSessionHandler appSessionHandler) 
        {
            _appSessionHandler = appSessionHandler;
        }

        public async Task OnGet()
        {
            await HttpContext.SignOutAsync("Cookie");
            await HttpContext.SignOutAsync("oidc", new AuthenticationProperties { RedirectUri = "Https://localhost:5002/" });
            _appSessionHandler.ClearSession();
        }
    }
}