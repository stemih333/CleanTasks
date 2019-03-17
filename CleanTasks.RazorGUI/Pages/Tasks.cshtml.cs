using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CleanTasks.RazorGUI.Pages
{
    [Authorize]
    public class TasksModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}