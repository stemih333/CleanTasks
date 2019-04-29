using CleanTasks.Application.Todo.Commands;
using CleanTasks.Application.Todo.Models;
using CleanTasks.Application.Todo.Queries;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Interfaces
{
    public interface ITodoApiClient
    {
        Task CreateTodoTask(CreateTodoCommand model);
        Task<PagedTodoResultDto> SearchTodos(TodoSearchQuery model);
        Task<PagedTodoResultDto> FilterTodos(TodoFilterSearchQuery model);
    }
}