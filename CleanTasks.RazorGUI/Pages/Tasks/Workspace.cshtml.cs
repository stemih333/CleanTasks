using CleanTasks.Application.Todo.Models;
using CleanTasks.Application.Todo.Queries;
using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Common.Constants;
using CleanTasks.RazorGUI.Attributes;
using CleanTasks.RazorGUI.Interfaces;
using CleanTasks.RazorGUI.Pages.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages
{
    [Authorize(Policy = Policies.All), ValidArea]
    public class WorkspaceModel : TasksBaseModel
    {
        public bool IsAdmin { get; set; }
        public List<TodoAreaDto> Areas { get; set; }
        
        public string CurrentAreaName { get; set; }
        public List<TodoDto> Todos { get; set; }

        [BindProperty(SupportsGet = true)]
        public TodoFilterSearchQuery SearchModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Refresh { get; set; }

        private readonly ITodoApiClient _todoApiClient;

        public WorkspaceModel(IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler, ITodoApiClient todoApiClient) 
            : base(authService, client, appSessionHandler) {
            _todoApiClient = todoApiClient;
        }
       
        public async Task OnGet()
        {
            IsAdmin = (await AuthService.AuthorizeAsync(User, Policies.Admin)).Succeeded;
            Areas = AppSessionHandler.GetData<List<TodoAreaDto>>(AreasKey);

            if (Areas == null || Refresh)
            {
                Areas = (IsAdmin) ? await TodoAreaClient.GetAllTodoAreas() : await GetAreasByPermission();
                AppSessionHandler.SetData(AreasKey, Areas);              
            }

            if (Areas != null && Areas.Any() && Id.HasValue) CurrentAreaName = Areas.FirstOrDefault(_ => _.TodoAreaId == Id)?.Name;

            if (Id.HasValue)
            {
                if (!SearchModel.CurrentPage.HasValue) SearchModel.CurrentPage = 1;
                if (!SearchModel.PageSize.HasValue) SearchModel.PageSize = 25;
                SearchModel.TodoAreaId = Id;
                var result = await _todoApiClient.FilterTodos(SearchModel);
                if (result != null)
                    Todos = result.Todos;
            }
        }

        private async Task<List<TodoAreaDto>> GetAreasByPermission()
        {
            var allowedAreas = User.Claims
                .Where(_ => _.Type.Equals(PermissionTypes.TodoAreaPermission))
                .Select(_ => _.Value)
                .ToList();

            return allowedAreas.Any() ? await TodoAreaClient.GetTodoAreas(allowedAreas) : null;
        }
    }
}