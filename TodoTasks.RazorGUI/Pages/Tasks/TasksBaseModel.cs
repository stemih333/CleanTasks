using TodoTasks.Application.ReferenceData.Models;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    public class TasksBaseModel : PageModel
    {
        protected const string AreasKey = "AreasCollection";
        protected const string UsersKey = "UsersCollection";
        protected const string ReferenceDataKey = "ReferenceData";
        protected readonly IAuthorizationService AuthService;
        protected readonly ITodoApiClient TodoApiClient;
        protected readonly IAppSessionHandler AppSessionHandler;

        public TasksBaseModel(IAuthorizationService authService, ITodoApiClient client, IAppSessionHandler appSessionHandler)
        {
            AuthService = authService;
            TodoApiClient = client;
            AppSessionHandler = appSessionHandler;
        }   
        
        protected async Task<ReferenceDataDto> GetReferenceData()
        {
            var refData = AppSessionHandler.GetData<ReferenceDataDto>(ReferenceDataKey);
            if (refData == null)
            {
                refData = await TodoApiClient.GetReferenceData();
                if (refData == null) throw new NullReferenceException("Failed to retrieve reference data from Todo API.");
            }

            return refData;
        }
    }
}
