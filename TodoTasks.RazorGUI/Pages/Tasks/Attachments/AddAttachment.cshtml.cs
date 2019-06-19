using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoTasks.Application.Attachment.Commands;
using TodoTasks.Common.Models;
using TodoTasks.DataAccess.Auth;
using TodoTasks.RazorGUI.Extensions;
using TodoTasks.RazorGUI.Interfaces;

namespace TodoTasks.RazorGUI.Pages.Tasks.Attachments
{
    [Authorize(Policy = Policies.All)]
    [RequestSizeLimit(10000000)]
    public class AddAttachmentModel : PageModel
    {
        [Required, BindProperty(SupportsGet = true), HiddenInput]
        public int? TodoId { get; set; }
        [StringLength(50), BindProperty]
        public string Description { get; set; }
        [Required(ErrorMessage = "A file must be selected."), BindProperty, Display(Name = "Attachment")]
        public IFormFile UploadedFile { get; set; }

        private readonly ITodoApiClient _todoClient;
        private readonly string _filePath;

        public AddAttachmentModel(ITodoApiClient todoClient, AppSettings settings)
        {
            _todoClient = todoClient;
            _filePath = settings.FilePath;
        }

        public void OnGet()
        {
            if (!TodoId.HasValue) throw new ArgumentException("Could add new attachment. Id value is missing.");
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            
            using (var reader = new BinaryReader(UploadedFile.OpenReadStream()))
            {
                var command = new CreateAttachmentCommand
                {
                    FileBytes = reader.ReadBytes((int)UploadedFile.Length),
                    FileName = UploadedFile.FileName,
                    UserId = User.GetUserId(),
                    FileSize = UploadedFile.Length,
                    FileType = UploadedFile.ContentType,
                    Description = Description,
                    FilePath = _filePath,
                    TodoId = TodoId.Value
                };

                var result = await _todoClient.CreateAttachment(command);

                if (result == 0) throw new Exception("Failed to insert file.");
            }

            return RedirectToPage("AttachmentManagement", new { TodoId });
        }
    }
}