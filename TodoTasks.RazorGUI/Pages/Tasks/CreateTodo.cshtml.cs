using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TodoTasks.Application.Todo.Commands;
using TodoTasks.Common;
using TodoTasks.RazorGUI.Attributes;
using TodoTasks.RazorGUI.Constants;
using TodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TodoTasks.DataAccess.Auth;
using System.Collections.Generic;
using TodoTasks.Domain.Entities;
using System.Linq;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.All), ValidArea]
    public class CreateTodoModel : TasksBaseModel
    {       
        [Required, BindProperty, HiddenInput]
        public int? CurrentArea { get; set; }
        [Required, BindProperty, StringLength(100)]
        public string Title { get; set; }
        [Required, BindProperty, StringLength(10000), HiddenInput]
        public string Description { get; set; }
        [BindProperty, StringLength(50), DisplayName("Assigned to")]
        public string AssignedTo { get; set; }
        [BindProperty, Required, DisplayName("Todo type")]
        public int? TodoTypeId { get; set; }
        [BindProperty]
        public bool Notify { get; set; }

        public SelectList Users { get; set; }
        public SelectList TodoTypes { get; set; }

        private readonly ITodoApiClient _todoClient;

        public CreateTodoModel(ITodoApiClient todoClient, IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler) : base(authService, client, appSessionHandler)
        {
            _todoClient = todoClient;
        }

        public async Task OnGet([Required]int? id)
        {
            if (!ModelState.IsValid) throw new ArgumentNullException("Could not create new Todo. Id value is missing.");
            AppSessionHandler.DeleteData(UsersKey);
            CurrentArea = id;

            Users = await GetUsers();
            TodoTypes = new SelectList((await GetReferenceData())?.Types, "Id", "Name");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) {

                Users = await GetUsers();
                TodoTypes = new SelectList((await GetReferenceData())?.Types, "Id", "Name");

                return Page();
            }
            
            var model = new CreateTodoCommand
            {
                AssignedTo = AssignedTo,
                Description = Description,
                Title = Title,
                TodoAreaId = CurrentArea,
                Type = (TodoType)TodoTypeId,
                UserId = User.Identity.Name,
                Notify = Notify
            };

            var newTodoId = await _todoClient.CreateTodoTask(model);
            TempData[ViewDataKeys.SuccessMessage] = $"Todo task added successfully.";
            return RedirectToPage("/Tasks/EditTodo", new { Id = newTodoId });
        }

        private async Task<SelectList> GetUsers()
        {
            var users = AppSessionHandler.GetData<List<AppUser>>(UsersKey);
            if (users == null)
            {
                var user = await _todoClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserPermission);
                var admins = await _todoClient.SearchUsers(AuthConstants.PermissionType, AuthConstants.UserAdminPermission);
                var allUsers = user.Concat(admins);
                AppSessionHandler.SetData(UsersKey, user.Concat(admins));
                users = allUsers.ToList();
            }

            if (users == null) throw new NullReferenceException("Could not retireve users from user API.");
            var userPermissions = (await _todoClient.SearchUsers(PermissionTypes.TodoAreaPermission, CurrentArea.Value.ToString()))?.Select(_ => _.UserName).ToList();
            if (userPermissions == null) throw new NullReferenceException("Could not retireve permissions from API.");

            return new SelectList(users.Where(_ => userPermissions.Contains(_.UserName)), "UserName", "UserName");
        }
    }
}