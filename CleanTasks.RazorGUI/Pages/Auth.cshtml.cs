using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CleanTasks.RazorGUI.Pages
{
    [Authorize]
    public class AuthModel : BaseModel
    {
        public async Task OnGet()
        {
            await HttpContext.SignOutAsync("Cookie");
            await HttpContext.SignOutAsync("oidc", new AuthenticationProperties { RedirectUri = "Https://localhost:5002/" });
        }
    }
}