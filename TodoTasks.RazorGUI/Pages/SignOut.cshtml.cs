using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace TodoTasks.RazorGUI.Pages
{
    public class SignOutModel : PageModel
    {
        private readonly IAppSessionHandler _appSessionHandler;

        public SignOutModel(IAppSessionHandler appSessionHandler) 
        {
            _appSessionHandler = appSessionHandler;
        }

        public IActionResult OnGet()
        {
            _appSessionHandler.ClearSession();

            return SignOut(
                new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}