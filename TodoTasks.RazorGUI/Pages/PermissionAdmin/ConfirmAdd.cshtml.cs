using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.RazorGUI.Exceptions;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TodoTasks.RazorGUI.Pages.PermissionAdmin
{
    public class ConfirmAddModel : BasePermissionAdminModel
    {
        
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string UserName { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? AreaId { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public string AreaName { get; set; }
        public ConfirmAddModel(ITodoApiClient todoApiClient) : base(todoApiClient)
        {}

        public void OnGet()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState, "Failed to show confirm page.");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException(ModelState, "Failed to add new area permission.");

            await TodoApiClient.CreateAreaPermission(AreaId, UserName);

            return RedirectToPage("/PermissionAdmin/Details", new { UserName });
        }
    }
}