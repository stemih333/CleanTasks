using TodoTasks.Application.Todo.Commands;
using TodoTasks.Application.Todo.Models;
using TodoTasks.Application.Todo.Queries;
using TodoTasks.Application.TodoComment.Commands;
using TodoTasks.Application.TodoTag.Commands;
using System.Threading.Tasks;

namespace TodoTasks.RazorGUI.Interfaces
{
    public interface ITodoApiClient
    {
        Task<int> CreateTodoTask(CreateTodoCommand model);
        Task<PagedTodoResultDto> SearchTodos(TodoSearchQuery model);
        Task<PagedTodoResultDto> FilterTodos(TodoFilterSearchQuery model);
        Task<int> CreateTodoComment(CreateTodoCommentCommand command);
        Task DeleteTodoComment(int? command);
        Task<int> EditTodoComment(CreateTodoCommentCommand command);
        Task DeleteTodoTag(int? command);
        Task<int> CreateTodoTag(CreateTodoTagCommand command);
        Task<int> EditTodoTask(EditTodoCommand model);
    }
}