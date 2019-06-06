using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.Application.Attachment.Models;
using TodoTasks.RazorGUI.Constants;
using TodoTasks.RazorGUI.Interfaces;

namespace TodoTasks.RazorGUI.Pages.Tasks.Attachments
{
    [Authorize(Policy = Policies.All)]
    public class AttachmentManagementModel : PageModel
    {
        [Required, BindProperty(SupportsGet = true), HiddenInput]
        public int? TodoId { get; set; }

        public IEnumerable<AttachmentDto> Attachments { get; set; }

        private readonly ITodoApiClient _todoClient;

        public AttachmentManagementModel(ITodoApiClient todoClient)
        {
            _todoClient = todoClient;
        }

        public async Task OnGet()
        {
            if (!ModelState.IsValid) throw new ArgumentException("Could manage attachments. Id value is missing.");

            Attachments = await _todoClient.GetAttachments(TodoId);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Attachments = await _todoClient.GetAttachments(TodoId);

                return Page();
            }


            return RedirectToPage("AddAttachmentConfirm");
        }
    }
}