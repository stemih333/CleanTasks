using TodoTasks.Application.TodoArea.Models;
using TodoTasks.RazorGUI.Exceptions;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using TodoTasks.Domain.Entities;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.RazorGUI.Pages.PermissionAdmin
{
    public class DetailsModel : BasePermissionAdminModel
    {
        [BindProperty(SupportsGet = true), Required]
        public string UserName { get; set; }
        public IEnumerable<AppPermission> Permissions { get; set; }
        public IEnumerable<TodoAreaDto> AvailableAreas { get; set; }
        public IEnumerable<TodoAreaDto> UserAreas { get; set; }

        public DetailsModel(ITodoApiClient apiClient) : base(apiClient)
        {
        }

        public async Task OnGet()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            var user = await TodoApiClient.GetPermissionUser(UserName);
            Permissions = user.Permissions.Where(_ => _.PermissionName.Equals(PermissionTypes.TodoAreaPermission));
            var areas = await TodoApiClient.GetTodoAreas(new string[] { });
            AvailableAreas = areas.Where(_ => !Permissions.Any(x => x.PermissionValue.Equals(_.TodoAreaId.Value.ToString())));
            UserAreas = areas.Where(_ => Permissions.Any(x => x.PermissionValue.Equals(_.TodoAreaId.Value.ToString())));
        }
    }
}