using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CleanTasks.Common.Constants;
using CleanTasks.CommonWeb.Helpers;
using TodoTasks.Application.Todo.Commands;
using TodoTasks.Application.Todo.Models;
using TodoTasks.Application.Todo.Queries;
using TodoTasks.Common;
using TodoTasks.RazorGUI.Interfaces;
using TodoTasks.RazorGUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TodoTasks.RazorGUI.Pages.Tasks
{
    [Authorize(Policy = Policies.All)]
    public class EditTodoModel : TasksBaseModel
    {
        [Required, BindProperty, HiddenInput]
        public int? TodoId { get; set; }
        [Required, BindProperty, StringLength(100)]
        public string Title { get; set; }
        [Required, BindProperty, StringLength(10000), HiddenInput]
        public string Description { get; set; }
        [BindProperty, StringLength(50), DisplayName("Assigned to")]
        public string AssignedTo { get; set; }
        [BindProperty, Required, DisplayName("Todo type")]
        public TodoType? TodoTypeId { get; set; }
        [BindProperty, DisplayName("Close reason")]
        public TodoReason? CloseReasonId { get; set; }
        [BindProperty, Required, DisplayName("Todo Status")]
        public TodoStatus? TodoStatusId { get; set; }
        [BindProperty]
        public bool Notify { get; set; }
        [BindProperty, DisplayName("Created by")]
        public string CreatedBy { get; set; }
        [BindProperty, DisplayName("Updated by")]
        public string UpdatedBy { get; set; }
        [BindProperty, DisplayName("Created date")]
        public string Created { get; set; }
        [BindProperty, DisplayName("Updated date")]
        public string Updated { get; set; }
        [BindProperty, HiddenInput]
        public int? TodoAreaId { get; set; }
        public IEnumerable<AuditedIdNameDto> Tags { get; set; }
        public IEnumerable<AuditedIdNameDto> Comments { get; set; }

        
        public SelectList UsersSelect { get; set; }
        public SelectList TodoTypesSelect { get; set; }
        public SelectList CloseReasonsSelect { get; set; }
        public SelectList TodoStatusesSelect { get; set; }
        public bool DisabledCloseReason { get; set; }

        private readonly IUserApiClient _userApiService;
        private readonly ITodoApiClient _todoClient;
        private const string TodoKey = "TodoKey";

        public EditTodoModel(ITodoApiClient todoClient, IAuthorizationService authService, ITodoAreaApiClient client, IAppSessionHandler appSessionHandler, IUserApiClient userApiService) : base(authService, client, appSessionHandler)
        {
            _userApiService = userApiService;
            _todoClient = todoClient;
        }

        public async Task OnGet([Required]int? id)
        {
            if (!ModelState.IsValid) throw new ArgumentNullException("Could not edit Todo. Id value is missing.");
            AppSessionHandler.DeleteData(TodoKey);

            TodoId = id;
            var todo = await GetTodo();
            await SetModelFromTodo(todo);            
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) {
                var todo = await GetTodo();
                await SetModelFromTodo(todo);

                return Page();
            }

            await _todoClient.EditTodoTask(new EditTodoCommand
            {
                AssignedTo = AssignedTo,
                CloseReasonId = CloseReasonId,
                Description = Description,
                Notify = Notify,
                Title = Title,
                TodoAreaId = TodoAreaId,
                TodoId = TodoId,
                TodoStatusId = TodoStatusId,
                Type = TodoTypeId,
                UserId = User.GetUserId()
            });

            return RedirectToPage("/Tasks/Workspace", new { Id = TodoAreaId });
        }

        private async Task<List<UserDto>> GetUsers()
        {
            var users = AppSessionHandler.GetData<List<UserDto>>(UsersKey);
            if (users == null)
            {
                users = await _userApiService.GetAllUsers();
                AppSessionHandler.SetData(UsersKey, users);
            }

            if (users == null) throw new NullReferenceException("Could not retireve users from user API.");

            users.Add(new UserDto { Id = User.GetUserId(), UserName = User.GetUserName() });

            return users;
        }

        private async Task<SelectList> GetUsersSelect(List<UserDto> users)
        {
            var userPermissions = (await TodoAreaClient.GetPermissionsByAreaId(TodoAreaId))?.Select(_ => _.UserId).ToList();
            if (userPermissions == null) throw new NullReferenceException("Could not retireve permissions from API.");

            return new SelectList(users.Where(_ => userPermissions.Contains(_.Id)), "Id", "UserName");
        }

        private async Task<TodoDto> GetTodo()
        {
            var result = await _todoClient.SearchTodos(new TodoSearchQuery { TodoId = TodoId, PageSize = 1, CurrentPage = 1 });
            if (result == null) throw new NullReferenceException("Failed to find todo task with ID: " + TodoId);
            var todo = result.Todos?.FirstOrDefault();
            if (todo == null) throw new NullReferenceException("Failed to find todo task with ID: " + TodoId);
            AppSessionHandler.SetData(TodoKey, todo);
            return todo;
        }

        private async Task SetModelFromTodo(TodoDto todo)
        {
            var users = await GetUsers();

            Title = todo.Title;
            Description = todo.Description;
            AssignedTo = todo.AssignedTo;
            CloseReasonId = todo.CloseReason;
            TodoTypeId = todo.Type;
            TodoStatusId = todo.Status;
            Notify = todo.Notify ?? false;
            CreatedBy = users.FirstOrDefault(x => x.Id.Equals(todo.CreatedBy))?.UserName ?? "Unknown";
            UpdatedBy = users.FirstOrDefault(x => x.Id.Equals(todo.UpdatedBy))?.UserName ?? "Unknown";
            Created = todo.Created?.ToString("yyyy-MM-dd");
            Updated = todo.Updated?.ToString("yyyy-MM-dd");
            Tags = todo.Tags;
            Comments = todo.Comments;
            TodoAreaId = todo.TodoAreaId;

            UsersSelect = await GetUsersSelect(users);

            var referenceData = await GetReferenceData();

            TodoTypesSelect = new SelectList(referenceData.Types, "Id", "Name");
            TodoStatusesSelect = new SelectList(referenceData.Statuses, "Id", "Name");

            if (TodoStatusId == TodoStatus.Closed)
            {
                CloseReasonsSelect = new SelectList(referenceData.Reasons, "Id", "Name");
            }

            DisabledCloseReason = TodoStatusId != TodoStatus.Closed;
        }
    }
}