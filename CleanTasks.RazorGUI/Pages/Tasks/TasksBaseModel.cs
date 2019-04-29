using CleanTasks.Application.ReferenceData.Models;
using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages.Tasks
{
    public class TasksBaseModel : PageModel
    {
        protected const string AreasKey = "AreasCollection";
        protected const string UsersKey = "UsersCollection";
        protected const string ReferenceDataKey = "ReferenceData";
        protected readonly IAuthorizationService AuthService;
        protected readonly ITodoAreaApiClient TodoAreaClient;
        protected readonly IAppSessionHandler AppSessionHandler;

        public TasksBaseModel(IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler)
        {
            AuthService = authService;
            TodoAreaClient = client;
            AppSessionHandler = appSessionHandler;
        }   
        
        protected async Task<ReferenceDataDto> GetReferenceData()
        {
            var refData = AppSessionHandler.GetData<ReferenceDataDto>(ReferenceDataKey);
            if (refData == null)
            {
                refData = await TodoAreaClient.GetReferenceData();
                if (refData == null) throw new NullReferenceException("Failed to retrieve reference data from Todo API.");
            }

            return refData;
        }
    }
}
