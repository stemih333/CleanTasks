using TodoTasks.Application.Todo.Models;
using TodoTasks.Application.Todo.Queries;
using TodoTasks.Application.TodoArea.Models;
using TodoTasks.RazorGUI.Attributes;
using TodoTasks.RazorGUI.Interfaces;
using TodoTasks.RazorGUI.Pages.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoTasks.DataAccess.Auth;
using TodoTasks.Domain.Entities;

namespace TodoTasks.RazorGUI.Pages
{
    [Authorize(Policy = Policies.User), ValidArea]
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

        public List<AppUser> Users { get; set; }

        private readonly ITodoApiClient _todoApiClient;

        public WorkspaceModel(IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler, ITodoApiClient todoApiClient) 
            : base(authService, client, appSessionHandler) {
            _todoApiClient = todoApiClient;
        }
       
        public async Task OnGet()
        {
            IsAdmin = (await AuthService.AuthorizeAsync(User, Policies.Admin)).Succeeded;
            Areas = await GetAreasByPermission();

            if (Areas != null && Areas.Any() && Id.HasValue) CurrentAreaName = Areas.FirstOrDefault(_ => _.TodoAreaId == Id)?.Name;

            if (Id.HasValue)
            {
                if (!SearchModel.CurrentPage.HasValue) SearchModel.CurrentPage = 1;
                if (!SearchModel.PageSize.HasValue) SearchModel.PageSize = 25;
                SearchModel.TodoAreaId = Id;
                var result = await _todoApiClient.FilterTodos(SearchModel);
                if (result != null)
                {
                    Todos = result.Todos;
                    Users = AppSessionHandler.GetData<List<AppUser>>(UsersKey);
                    if(Users == null)
                    {
                        var admins = await _todoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserAdminPermission);
                        var appUsers = await _todoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserPermission);
                        Users = admins.Concat(appUsers).ToList();                     
                        AppSessionHandler.SetData(UsersKey, Users);
                    }

                    //Users.Add(new UserDto { Id = User.GetUserId(), UserName = User.GetUserName() });
                }                   
            }
        }

        private async Task<List<TodoAreaDto>> GetAreasByPermission()
        {
            var user = await _todoApiClient.GetPermissionUser(User.Identity.Name);
            if (user == null) return null;
            var areas = await TodoAreaClient.GetTodoAreas(user.Permissions.Where(_ => _.PermissionName.Equals(PermissionTypes.TodoAreaPermission)).Select(_ => _.PermissionValue));
            return areas;
        }
    }
}