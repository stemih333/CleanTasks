using TodoTasks.Application.Todo.Models;
using TodoTasks.Application.Todo.Queries;
using TodoTasks.Application.TodoArea.Models;
using TodoTasks.RazorGUI.Attributes;
using TodoTasks.RazorGUI.Interfaces;
using TodoTasks.RazorGUI.Models;
using TodoTasks.RazorGUI.Pages.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoTasks.RazorGUI.Extensions;
using TodoTasks.RazorGUI.Constants;

namespace TodoTasks.RazorGUI.Pages
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

        public List<UserDto> Users { get; set; }

        private readonly ITodoApiClient _todoApiClient;
        private readonly IUserApiClient _userApiClient;

        public WorkspaceModel(IUserApiClient userApiClient, IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler, ITodoApiClient todoApiClient) 
            : base(authService, client, appSessionHandler) {
            _todoApiClient = todoApiClient;
            _userApiClient = userApiClient;
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
                    Users = AppSessionHandler.GetData<List<UserDto>>(UsersKey);
                    if(Users == null)
                    {
                        Users = await _userApiClient.GetAllUsers();                     
                        AppSessionHandler.SetData(UsersKey, Users);
                    }

                    Users.Add(new UserDto { Id = User.GetUserId(), UserName = User.GetUserName() });

                    Todos = Todos.Select(_ => {
                        _.AssignedTo = Users.FirstOrDefault(x => x.Id.Equals(_.AssignedTo))?.UserName;
                        _.CreatedBy = Users.FirstOrDefault(x => x.Id.Equals(_.CreatedBy))?.UserName ?? "Unknown";
                        _.UpdatedBy = Users.FirstOrDefault(x => x.Id.Equals(_.UpdatedBy))?.UserName ?? "Unknown";
                        return _;
                    }).ToList();
                }                   
            }
        }

        private async Task<List<TodoAreaDto>> GetAreasByPermission()
        {
            var areaPermissions = (await TodoAreaClient.GetPermissionsByUserId(User.GetUserId()))?.Select(_ => _.TodoAreaId.ToString()).ToList();

            return areaPermissions.Any() ? await TodoAreaClient.GetTodoAreas(areaPermissions) : null;
        }
    }
}