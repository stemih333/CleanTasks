using TodoTasks.Application.Todo.Commands;
using TodoTasks.Application.Todo.Models;
using TodoTasks.Application.Todo.Queries;
using TodoTasks.Application.TodoComment.Commands;
using TodoTasks.Application.TodoTag.Commands;
using System.Threading.Tasks;
using TodoTasks.Application.Attachment.Models;
using System.Collections.Generic;
using TodoTasks.Application.Attachment.Commands;
using TodoTasks.Domain.Entities;
using TodoTasks.Application.TodoArea.Models;
using TodoTasks.Application.ReferenceData.Models;

namespace TodoTasks.RazorGUI.Interfaces
{
    public interface ITodoApiClient
    {
        Task<int> CreateTodoTask(CreateTodoCommand model);
        Task<PagedTodoResultDto> SearchTodos(TodoSearchQuery model);
        Task<PagedTodoResultDto> FilterTodos(TodoFilterSearchQuery model);
        Task<int> CreateTodoComment(CreateTodoCommentCommand command);
        Task DeleteTodoComment(int? commentId);
        Task DeleteAttachment(int? attachmentId);
        Task<int> EditTodoComment(CreateTodoCommentCommand command);
        Task DeleteTodoTag(int? command);
        Task<int> CreateTodoTag(CreateTodoTagCommand command);
        Task<int> EditTodoTask(EditTodoCommand model);
        Task<int> CreateAttachment(CreateAttachmentCommand command);
        Task<IEnumerable<AttachmentDto>> GetAttachments(int? todoId);
        Task<AttachmentDto> GetAttachment(int? attachmentId);
        Task<IEnumerable<AppUser>> SearchUsers(string claimType, string claimValue);
        Task<PermissionUser> GetPermissionUser(string username);
        Task<IEnumerable<TodoAreaDto>> GetTodoAreas(IEnumerable<string> allowedAreas);
        Task CreateTodoArea(string areaName, string userName);
        Task EditTodoArea(string areaName, int areaId, string userName);
        Task DeleteTodoArea(int areaId);
        Task<bool> AreaExist(string id);
        Task<ReferenceDataDto> GetReferenceData();
        Task CreateAreaPermission(int? areaId, string username);
        Task DeleteAreaPermission(int? areaId, string username);
    }
}