using CleanTasks.Application.TodoArea.Models;
using CleanTasks.RazorGUI.Constants;
using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages.Tasks
{
    public class DeleteModel : TasksBaseModel
    {
        public string AreaName { get; set; }

        [BindProperty, Required, HiddenInput]
        public int? Id { get; set; }

        public DeleteModel(IAuthorizationService authService, ITodoApiClient client, IAppSessionHandler appSessionHandler) : base(authService, client, appSessionHandler)
        { }

        public void OnGet(int id)
        {
            var tasks = AppSessionHandler.GetData<List<TodoAreaDto>>(AreasKey);

            if(tasks != null)
            {
                var area = tasks.FirstOrDefault(_ => _.TodoAreaId == id)?.Name;
                AreaName = (string.IsNullOrEmpty(AreaName))
                    ? $"name '{area}'"
                    : $"ID '{id}'";
            }
            else
            {
                AreaName = $"ID '{id}'";
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await Client.DeleteTodoArea(Id.Value);

                AppSessionHandler.DeleteData(AreasKey);
                TempData[ViewDataKeys.SuccessMessage] = $"Area deleted successfully.";
                return RedirectToPage("/Tasks/Workspace");
            }

            throw new ArgumentNullException("Could not delete area. Id value is missing.");
        }
    }
}