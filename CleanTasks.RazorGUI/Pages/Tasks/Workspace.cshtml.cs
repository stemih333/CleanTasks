using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Common.Constants;
using CleanTasks.RazorGUI.Interfaces;
using CleanTasks.RazorGUI.Pages.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages
{
    [Authorize(Policy = Policies.All)]
    public class WorkspaceModel : TasksBaseModel
    {
        public bool IsAdmin { get; set; }
        public List<TodoAreaDto> Areas { get; set; }
        public int? CurrentArea { get; set; }
        public string CurrentAreaName { get; set; }

        public WorkspaceModel(IAuthorizationService authService, ITodoApiClient client, IAppSessionHandler appSessionHandler) 
            : base(authService, client, appSessionHandler) {}

        public async Task OnGet(int? id, [FromQuery]bool refresh)
        {
            if(id.HasValue)
            {
                if (!await Client.AreaExist(id.Value)) throw new Exception($"Area id '{id.Value}' does not exist.");
            }

            CurrentArea = id;
            IsAdmin = (await AuthService.AuthorizeAsync(User, Policies.Admin)).Succeeded;
            Areas = AppSessionHandler.GetData<List<TodoAreaDto>>(AreasKey);

            if (Areas == null || refresh)
            {
                Areas = (IsAdmin) ? await Client.GetAllTodoAreas() : await GetAreasByPermission();
                AppSessionHandler.SetData(AreasKey, Areas);              
            }

            if (Areas != null && Areas.Any() && CurrentArea.HasValue) CurrentAreaName = Areas.FirstOrDefault(_ => _.TodoAreaId == CurrentArea)?.Name;
        }

        private async Task<List<TodoAreaDto>> GetAreasByPermission()
        {
            var allowedAreas = User.Claims
                .Where(_ => _.Type.Equals(PermissionTypes.TodoAreaPermission))
                .Select(_ => _.Value)
                .ToList();

            return allowedAreas.Any() ? await Client.GetTodoAreas(allowedAreas) : null;
        }
    }
}