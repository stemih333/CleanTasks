﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoTasks.DataAccess.Auth;
using TodoTasks.RazorGUI.Interfaces;

namespace TodoTasks.RazorGUI.Pages.Tasks.Attachments
{
    [Authorize(Policy = Policies.User)]
    public class DownloadAttachmentModel : PageModel
    {
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? AttachmentId { get; set; }
        [Required, BindProperty(SupportsGet = true), HiddenInput]
        public int? TodoId { get; set; }

        private readonly ITodoApiClient _todoClient;

        public DownloadAttachmentModel(ITodoApiClient todoClient)
        {
            _todoClient = todoClient;
        }

        public void OnGet()
        {
            if (!ModelState.IsValid) throw new ArgumentException("Could not download attachment. Id value is missing.");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) throw new ArgumentException("Could not download attachment. Id value is missing.");

            var file = await _todoClient.GetAttachment(AttachmentId);

            return File(file.FileBytes, file.Type, file.Name);
        }
    }
}