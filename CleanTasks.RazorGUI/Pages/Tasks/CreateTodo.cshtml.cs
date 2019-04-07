using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CleanTasks.Application.Interfaces;
using CleanTasks.Common.Constants;
using CleanTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CleanTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.All)]
    public class CreateTodoModel : PageModel
    {
        [Required, BindProperty, HiddenInput]
        public int? CurrentArea { get; set; }
        [Required, BindProperty, StringLength(100)]
        public string Title { get; set; }
        [Required, BindProperty, StringLength(10000), HiddenInput]
        public string Description { get; set; }
        [BindProperty, StringLength(25)]
        public string AssignedTo { get; set; }
        [BindProperty, StringLength(100)]
        public string AssignedToName { get; set; }

        private readonly IUserApiService _userService;

        public CreateTodoModel(IUserApiService userService)
        {
            _userService = userService;
        }

        public async Task OnGet([Required]int? id)
        {
            if (!ModelState.IsValid) throw new ArgumentNullException("Could not create new Todo. Id value is missing.");

            var users = await _userService.GetUsersByArea(id.Value);

            CurrentArea = id;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            return RedirectToPage("Workspace", CurrentArea);
        }
    }
}