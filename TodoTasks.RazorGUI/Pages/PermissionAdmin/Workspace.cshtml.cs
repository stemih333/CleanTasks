using TodoTasks.RazorGUI.Extensions;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoTasks.Domain.Entities;
using System.Linq;
using TodoTasks.OpenIdConnectAuth.Auth;
using TodoTasks.Application.Comparers;

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
        public WorkspaceModel(ITodoApiClient apiClient) : base(apiClient)
        {
        }

        public async Task OnGet()
        {           
            var admins = await TodoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserAdminPermission);
            var appUsers = await TodoApiClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserPermission);
            Users = admins.Union(appUsers, new AppUserComparer()).ToList();
            
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