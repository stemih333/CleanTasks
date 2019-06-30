using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.Application.TodoComment.Commands;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoTasks.DataAccess.Auth;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.All)]
    public class AddCommentModel : PageModel
    {
        [BindProperty, Required, HiddenInput]
        public int? TodoId { get; set; }
        [Required, BindProperty(), StringLength(300)]
        public string Comment { get; set; }

        private readonly ITodoApiClient todoApiClient;

        public AddCommentModel(ITodoApiClient todoApiClient)
        {
            this.todoApiClient = todoApiClient;
        }

        public void OnGet([Required]int? id) {
            if (!ModelState.IsValid) throw new NullReferenceException("Cannot write comment. Id is missing.");

            TodoId = id;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            await todoApiClient.CreateTodoComment(new CreateTodoCommentCommand
            {
                TodoId = TodoId,
                UserId = User.Identity.Name,
                Value = Comment
            });

            return RedirectToPage("/Tasks/EditTodo", new { Id = TodoId });
        }
    }
}