using TodoTasks.Application.TodoArea.Models;
using TodoTasks.RazorGUI.Attributes;
using TodoTasks.RazorGUI.Constants;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoTasks.DataAccess.Auth;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.Admin), ValidArea]
    public class DeleteAreaModel : TasksBaseModel
    {
        public string AreaName { get; set; }

        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? Id { get; set; }

        public DeleteAreaModel(IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler) : base(authService, client, appSessionHandler)
        { }

        public void OnGet()
        {
            if (!ModelState.IsValid) throw new NullReferenceException("Id value is missing.");

            var tasks = AppSessionHandler.GetData<List<TodoAreaDto>>(AreasKey);

            if(tasks != null)
            {
                var area = tasks.FirstOrDefault(_ => _.TodoAreaId == Id)?.Name;
                AreaName = (string.IsNullOrEmpty(AreaName))
                    ? $"name '{area}'"
                    : $"ID '{Id}'";
            }
            else
            {
                AreaName = $"ID '{Id}'";
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await TodoAreaClient.DeleteTodoArea(Id.Value);

                AppSessionHandler.DeleteData(AreasKey);
                TempData[ViewDataKeys.SuccessMessage] = $"Area deleted successfully.";
                return RedirectToPage("/Tasks/Workspace");
            }

            throw new ArgumentNullException("Could not delete area. Id value is missing.");
        }
    }
}