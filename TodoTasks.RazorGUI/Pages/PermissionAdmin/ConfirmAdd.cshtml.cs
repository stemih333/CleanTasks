using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.RazorGUI.Exceptions;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TodoTasks.RazorGUI.Extensions;

namespace TodoTasks.RazorGUI.Pages.PermissionAdmin
{
    public class ConfirmAddModel : BasePermissionAdminModel
    {
        
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string UserName { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string UserId { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? AreaId { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string AreaName { get; set; }
        public ConfirmAddModel(ITodoAreaApiClient todoAreaApiClient, IAppSessionHandler appSessionHandler, IUserApiClient userApiService) : base(todoAreaApiClient, appSessionHandler, userApiService)
        {}

        public void OnGet()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState, "Failed to show confirm page.");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState, "Failed to add new area permission.");

            await TodoAreaApiClient.CreateAreaPermission(AreaId, UserId, User.GetUserId());

            return RedirectToPage("/PermissionAdmin/Details", new { UserId, UserName });
        }
    }
}