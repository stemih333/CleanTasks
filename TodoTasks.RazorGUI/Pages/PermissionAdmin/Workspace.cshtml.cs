using TodoTasks.RazorGUI.Extensions;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoTasks.Domain.Entities;
using System.Linq;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.RazorGUI.Pages.PermissionAdmin
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

        public IEnumerable<AppUser> Users { get; set; }

        public ITodoApiClient _todoApiClient { get; set; }
        public WorkspaceModel(ITodoAreaApiClient todoAreaApiClient, ITodoApiClient apiClient) : base(todoAreaApiClient)
        {
            _todoApiClient = apiClient;
        }

        public async Task OnGet()
        {           
            var admins = await _todoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserAdminPermission);
            var appUsers = await _todoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserPermission);
            Users = admins.Concat(appUsers).ToList();
            
            if (!string.IsNullOrEmpty(Sort))
            {
                if(Sort == CurrentSort)
                {
                    SortOrder = SortOrder.Equals("asc") ? "desc" : "asc";
                }
                else
                {
                    SortOrder = "asc";
                }
                Users = Users.OrderByString(Sort, SortOrder);
            }
            CurrentSort = Sort;
        }
    }
}