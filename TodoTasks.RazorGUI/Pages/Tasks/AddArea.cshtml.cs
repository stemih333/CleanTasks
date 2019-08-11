using TodoTasks.RazorGUI.Constants;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.Admin)]
    public class AddAreaModel : TasksBaseModel
    {
        public AddAreaModel(IAuthorizationService authService, ITodoApiClient client, IAppSessionHandler appSessionHandler) : base(authService, client, appSessionHandler)
        { }

        [BindProperty, Required, StringLength(50, MinimumLength = 2), Display(Name = "Area name")]
        public string AreaName { get; set; }

        public void OnGet() {}

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                await TodoApiClient.CreateTodoArea(AreaName, User.Identity.Name);

                AppSessionHandler.DeleteData(AreasKey);
                TempData[ViewDataKeys.SuccessMessage] = $"Area '{AreaName}' created successfully.";
                return RedirectToPage("/Tasks/Workspace");
            }

            return Page();
        }
    }
}