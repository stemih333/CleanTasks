using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.OpenIdConnectAuth.Auth;
using TodoTasks.RazorGUI.Interfaces;

namespace TodoTasks.RazorGUI.Pages.Tasks.Attachments
{
    [Authorize(Policy = Policies.User)]
    public class DeleteAttachmentModel : PageModel
    {
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? AttachmentId { get; set; }
        [Required, BindProperty(SupportsGet = true), HiddenInput]
        public int? TodoId { get; set; }

        private readonly ITodoApiClient _todoClient;

        public DeleteAttachmentModel(ITodoApiClient todoClient)
        {
            _todoClient = todoClient;
        }

        public void OnGet()
        {
            if (!ModelState.IsValid) throw new ArgumentException("Could not delete attachment. Id value is missing.");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) throw new ArgumentException("Could not delete attachment. Id value is missing.");

            await _todoClient.DeleteAttachment(AttachmentId);

            return RedirectToPage("AttachmentManagement", new { TodoId });
        } 
    }
}