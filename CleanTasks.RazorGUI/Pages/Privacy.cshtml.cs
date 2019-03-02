using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace CleanTasks.RazorGUI.Pages
{
    [Authorize]
    public class PrivacyModel : BaseModel
    {
        public void OnGet()
        {
            
        }
    }
}