using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Application.TodoAreaPermissions.Models;
using CleanTasks.RazorGUI.Exceptions;
using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages.PermissionAdmin
{
    public class DetailsModel : BasePermissionAdminModel
    {
        [BindProperty(SupportsGet = true), Required]
        public string UserId { get; set; }

        [BindProperty(SupportsGet = true), Required]
        public string UserName { get; set; }

        public IEnumerable<TodoAreaPermissionDto> Permissions { get; set; }
        public IEnumerable<TodoAreaDto> AvailableAreas { get; set; }

        public DetailsModel(ITodoAreaApiClient todoAreaApiClient, IAppSessionHandler appSessionHandler, IUserApiService userApiService) : base(todoAreaApiClient, appSessionHandler, userApiService)
        {}

        public async Task OnGet()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState);

            Permissions = await TodoAreaApiClient.GetPermissionsByUserId(UserId);

            var areas = await TodoAreaApiClient.GetAllTodoAreas();
            AvailableAreas = areas.Where(_ => !Permissions.Any(x => x.TodoAreaId == _.TodoAreaId));
            
        }
    }
}