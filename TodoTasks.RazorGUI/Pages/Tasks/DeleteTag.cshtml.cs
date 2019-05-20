using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoTasks.RazorGUI.Constants;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.All)]
    public class DeleteTagModel : PageModel
    {
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? Id { get; set; }
        [BindProperty(SupportsGet = true), Required, HiddenInput]
        public int? TodoId { get; set; }

        private readonly ITodoApiClient todoApiClient;

        public DeleteTagModel(ITodoApiClient todoApiClient)
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

            await todoApiClient.DeleteTodoTag(Id);

            return RedirectToPage("/Tasks/EditTodo", new { Id = TodoId });
        }
    }
}