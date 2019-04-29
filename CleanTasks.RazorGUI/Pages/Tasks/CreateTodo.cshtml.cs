using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CleanTasks.Application.Todo.Commands;
using CleanTasks.Common.Constants;
using CleanTasks.CommonWeb.Helpers;
using CleanTasks.Domain.Enums;
using CleanTasks.RazorGUI.Attributes;
using CleanTasks.RazorGUI.Constants;
using CleanTasks.RazorGUI.Interfaces;
using CleanTasks.RazorGUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanTasks.RazorGUI.Pages.Tasks
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
        [BindProperty, StringLength(25), DisplayName("Assigned to")]
        public string AssignedTo { get; set; }
        [BindProperty, Required, DisplayName("Todo type")]
        public int? TodoTypeId { get; set; }
        [BindProperty]
        public bool Notify { get; set; }

        public SelectList Users { get; set; }
        public SelectList TodoTypes { get; set; }

        private readonly IUserApiService _userApiService;
        private readonly ITodoApiClient _todoClient;

        public CreateTodoModel(ITodoApiClient todoClient, IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler, IUserApiService userApiService) : base(authService, client, appSessionHandler)
        {
            _userApiService = userApiService;
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
                Type = (TodoTypes)TodoTypeId,
                UserName = User.GetUserName(),
                Notify = Notify
            };

            await _todoClient.CreateTodoTask(model);
            TempData[ViewDataKeys.SuccessMessage] = $"Todo task added successfully.";
            return RedirectToPage("Workspace", new { Id = CurrentArea.Value });
        }
  
        private async Task<SelectList> GetUsers()
        {
            var users = AppSessionHandler.GetData<List<UserDto>>(UsersKey);
            if (users == null)
            {
                users = await _userApiService.GetAllUsers();
                AppSessionHandler.SetData(UsersKey, users);
            }
        
            if (users == null) throw new NullReferenceException("Could not retireve users from user API.");
            var userPermissions = (await TodoAreaClient.GetPermissionsByAreaId(CurrentArea))?.Select(_ => _.UserId).ToList();
            if (userPermissions == null) throw new NullReferenceException("Could not retireve permissions from API.");
            
            return new SelectList(users.Where(_ => userPermissions.Contains(_.Id)), "Id", "UserName");
        }
    }
}