using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages.Tasks
{
    public class TasksBaseModel : PageModel
    {
        protected const string AreasKey = "AreasCollection";
        protected readonly IAuthorizationService AuthService;
        protected readonly ITodoApiClient Client;
        protected readonly IAppSessionHandler AppSessionHandler;

        public TasksBaseModel(IAuthorizationService authService, ITodoApiClient client, IAppSessionHandler appSessionHandler)
        {
            AuthService = authService;
            Client = client;
            AppSessionHandler = appSessionHandler;
        }       
    }
}
