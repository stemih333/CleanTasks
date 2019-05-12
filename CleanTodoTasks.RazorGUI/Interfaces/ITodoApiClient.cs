using CleanTodoTasks.Application.Todo.Commands;
using CleanTodoTasks.Application.Todo.Models;
using CleanTodoTasks.Application.Todo.Queries;
using CleanTodoTasks.Application.TodoComment.Commands;
using CleanTodoTasks.Application.TodoTag.Commands;
using System.Threading.Tasks;

namespace CleanTodoTasks.RazorGUI.Interfaces
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