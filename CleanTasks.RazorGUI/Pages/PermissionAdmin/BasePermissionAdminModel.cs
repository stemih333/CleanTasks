using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CleanTasks.RazorGUI.Pages.PermissionAdmin
{
    public class BasePermissionAdminModel : PageModel
    {
        protected readonly ITodoAreaApiClient TodoAreaApiClient;
        protected readonly IAppSessionHandler AppSessionHandler;
        protected readonly IUserApiService UserApiService;
        protected const string AllUserKey = "AllUserKey";
        protected const string AreaPermissionKey = "AreaPermissionKey";

        public BasePermissionAdminModel(ITodoAreaApiClient todoAreaApiClient, IAppSessionHandler appSessionHandler, IUserApiService userApiService)
        {
            TodoAreaApiClient = todoAreaApiClient;
            AppSessionHandler = appSessionHandler;
            UserApiService = userApiService;
        }
    }
}
