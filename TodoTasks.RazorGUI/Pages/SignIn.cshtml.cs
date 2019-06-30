using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace TodoTasks.RazorGUI.Pages
{
    public class SignInModel : PageModel
    {
        public IActionResult OnGet()
       => Challenge(new AuthenticationProperties { RedirectUri = "/SignIn?handler=signInUser" }, OpenIdConnectDefaults.AuthenticationScheme);

        public IActionResult OnGetSignInUser()
        {
            if (!User.Identity.IsAuthenticated) throw new Exception("User not authenticated.");            

            return RedirectToPage("/Index");
        }
    }
}