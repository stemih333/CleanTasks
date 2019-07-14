using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.Application.TodoTag.Commands;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoTasks.DataAccess.Auth;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.User)]
    public class AddTagModel : PageModel
    {
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? TodoId { get; set; }

        [BindProperty(), Required, DisplayName("Tag Text"), StringLength(20)]
        public string TagText { get; set; }

        private readonly ITodoApiClient todoApiClient;

        public AddTagModel(ITodoApiClient todoApiClient)
        {
            this.todoApiClient = todoApiClient;
        }

        public void OnGet()
        {
            if (!ModelState.IsValid) throw new NullReferenceException("Cannot write comment. Id is missing.");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            await todoApiClient.CreateTodoTag(new CreateTodoTagCommand
            {
                TodoId = TodoId,
                UserId = User.Identity.Name,
                Value = TagText
            });

            return RedirectToPage("/Tasks/EditTodo", new { Id = TodoId });
        }
    }
}