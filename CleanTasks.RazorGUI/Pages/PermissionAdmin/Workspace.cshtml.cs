using CleanTasks.Application.TodoAreaPermissions.Models;
using CleanTasks.Common.Constants;
using CleanTasks.RazorGUI.Extensions;
using CleanTasks.RazorGUI.Interfaces;
using CleanTasks.RazorGUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages.PermissionAdmin
{
    [Authorize(Policy = Policies.Admin)]
    public class WorkspaceModel : BasePermissionAdminModel
    {

        [BindProperty(SupportsGet = true)]
        public string Sort { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = "asc";

        [BindProperty(SupportsGet = true)]
        public string CurrentSort { get; set; } = "";

        public IEnumerable<TodoPermissionModel> UserPermissions { get; set; }

        public WorkspaceModel(ITodoAreaApiClient todoAreaApiClient, IAppSessionHandler appSessionHandler, IUserApiService userApiService) : base(todoAreaApiClient, appSessionHandler, userApiService)
        {
        }

        public async Task OnGet()
        {
            var users = AppSessionHandler.GetData<List<UserDto>>(AllUserKey);
            if(users == null)
            {
                users = await UserApiService.GetAllUsers();
                AppSessionHandler.SetData(AllUserKey, users);
            }

            var permissions = AppSessionHandler.GetData<List<TodoAreaPermissionDto>>(AreaPermissionKey);
            if(permissions == null)
            {
                permissions = await TodoAreaApiClient.GetAllPermissions();
                AppSessionHandler.SetData(AreaPermissionKey, permissions);
            }

            UserPermissions = users.Select(_ => new TodoPermissionModel {
                Id = _.Id,
                FirstName = _.FirstName,
                LastName = _.LastName,
                UserName = _.UserName,
                PermittedAreas = permissions.Where(x => x.UserId.Equals(_.Id)).Select(x => x.AreaName).ToList()
            });

            if(!string.IsNullOrEmpty(Sort))
            {
                if(Sort == CurrentSort)
                {
                    SortOrder = SortOrder.Equals("asc") ? "desc" : "asc";
                }
                else
                {
                    SortOrder = "asc";
                }
                UserPermissions = UserPermissions.OrderByString(Sort, SortOrder);
            }
            CurrentSort = Sort;
        }
    }
}