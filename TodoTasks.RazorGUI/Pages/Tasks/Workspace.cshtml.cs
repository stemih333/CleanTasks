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
using TodoTasks.Domain.Entities;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.RazorGUI.Pages
{
    [Authorize(Policy = Policies.User), ValidArea]
    public class WorkspaceModel : TasksBaseModel
    {
        public bool IsAdmin { get; set; }
        public IEnumerable<TodoAreaDto> Areas { get; set; }
        
        public string CurrentAreaName { get; set; }
        public IEnumerable<TodoDto> Todos { get; set; }

        [BindProperty(SupportsGet = true)]
        public TodoFilterSearchQuery SearchModel { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Refresh { get; set; }

        public List<AppUser> Users { get; set; }

        public WorkspaceModel(IAuthorizationService authService, ITodoApiClient client, IAppSessionHandler appSessionHandler) 
            : base(authService, client, appSessionHandler) {
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
                var result = await TodoApiClient.FilterTodos(SearchModel);
                if (result != null)
                {
                    Todos = result.Todos;
                    Users = AppSessionHandler.GetData<List<AppUser>>(UsersKey);
                    if(Users == null)
                    {
                        var admins = await TodoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserAdminPermission);
                        var appUsers = await TodoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserPermission);
                        Users = admins.Concat(appUsers).ToList();                     
                        AppSessionHandler.SetData(UsersKey, Users);
                    }

                    //Users.Add(new UserDto { Id = User.GetUserId(), UserName = User.GetUserName() });
                }                   
            }
        }

        private async Task<IEnumerable<TodoAreaDto>> GetAreasByPermission()
        {
            var user = await TodoApiClient.GetPermissionUser(User.Identity.Name);
            if (user == null) return null;
            var areas = await TodoApiClient.GetTodoAreas(user.Permissions.Where(_ => _.PermissionName.Equals(PermissionTypes.TodoAreaPermission)).Select(_ => _.PermissionValue));
            return areas;
        }
    }
}