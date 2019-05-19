using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoTasks.RazorGUI.Pages.PermissionAdmin
{
    public class BasePermissionAdminModel : PageModel
    {
        protected readonly ITodoAreaApiClient TodoAreaApiClient;
        protected readonly IAppSessionHandler AppSessionHandler;
        protected readonly IUserApiClient UserApiService;
        protected const string AllUserKey = "AllUserKey";
        protected const string AreaPermissionKey = "AreaPermissionKey";

        public BasePermissionAdminModel(ITodoAreaApiClient todoAreaApiClient, IAppSessionHandler appSessionHandler, IUserApiClient userApiService)
        {
            TodoAreaApiClient = todoAreaApiClient;
            AppSessionHandler = appSessionHandler;
            UserApiService = userApiService;
        }
    }
}
