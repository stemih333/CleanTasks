using CleanTasks.Common.Constants;
using CleanTasks.CommonWeb.Helpers;
using CleanTodoTasks.RazorGUI.Attributes;
using CleanTodoTasks.RazorGUI.Constants;
using CleanTodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CleanTodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.Admin), ValidArea]
    public class EditModel : TasksBaseModel
    {
        [BindProperty, Required, HiddenInput]
        public int? CurrentAreaId { get; set; }
        [BindProperty, Required, StringLength(50, MinimumLength = 2), Display(Name = "New area name")]
        public string NewAreaName { get; set; }
        public string OldAreaName { get; set; }

        public EditModel(IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler) : base(authService, client, appSessionHandler)
        { }

        public async Task OnGet([Required]int? id)
        {
            if (!ModelState.IsValid) throw new ArgumentNullException("Could not edit Todo Area. Id value is missing.");

            var areas = await TodoAreaClient.GetTodoAreas(new List<string> { id.Value.ToString() });

            if(areas == null || !areas.Any()) throw new NullReferenceException($"Could not find any areas with ID {id.Value}.");

            var area = areas.FirstOrDefault();

            CurrentAreaId = area.TodoAreaId.Value;
            OldAreaName = area.Name;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await TodoAreaClient.EditTodoArea(NewAreaName, CurrentAreaId.Value, User.GetUserId());

                AppSessionHandler.DeleteData(AreasKey);
                TempData[ViewDataKeys.SuccessMessage] = $"Area name changed successfully.";
                return RedirectToPage("/Tasks/Workspace");
            }

            return Page();
        }
    }
}