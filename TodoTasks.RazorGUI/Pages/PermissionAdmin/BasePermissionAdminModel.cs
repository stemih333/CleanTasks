using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoTasks.RazorGUI.Pages.PermissionAdmin
{
    public class BasePermissionAdminModel : PageModel
    {
        protected readonly ITodoAreaApiClient TodoAreaApiClient;
        protected const string AllUserKey = "AllUserKey";
        protected const string AreaPermissionKey = "AreaPermissionKey";

        public BasePermissionAdminModel(ITodoAreaApiClient todoAreaApiClient)
        {
            TodoAreaApiClient = todoAreaApiClient;
        }
    }
}
