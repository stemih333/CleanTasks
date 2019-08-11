using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoTasks.RazorGUI.Pages.PermissionAdmin
{
    public class BasePermissionAdminModel : PageModel
    {
        protected readonly ITodoApiClient TodoApiClient;
        protected const string AllUserKey = "AllUserKey";
        protected const string AreaPermissionKey = "AreaPermissionKey";

        public BasePermissionAdminModel(ITodoApiClient todoApiClient)
        {
            TodoApiClient = todoApiClient;
        }
    }
}
