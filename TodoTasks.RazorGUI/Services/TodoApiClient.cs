using System.Net.Http;
using TodoTasks.RazorGUI.Interfaces;
using System.Threading.Tasks;
using TodoTasks.Application.Todo.Commands;
using TodoTasks.Application.Todo.Models;
using TodoTasks.Application.Todo.Queries;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;
using TodoTasks.Application.TodoComment.Commands;
using TodoTasks.Application.TodoTag.Commands;

namespace TodoTasks.RazorGUI.Services {
    public class TodoApiClient : ITodoApiClient {
        private readonly HttpClient _client;

        public TodoApiClient (HttpClient client) {
            _client = client;
        }

        public async Task<int> CreateTodoComment(CreateTodoCommentCommand command)
        {
            var client = await _client.PutAsJsonAsync("api/todocomment", command);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<int>();
        }

        public async Task<int> CreateTodoTag(CreateTodoTagCommand command)
        {
            var client = await _client.PutAsJsonAsync("api/todotag", command);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<int>();
        }

        public async Task<int> CreateTodoTask(CreateTodoCommand model) {
            var client = await _client.PutAsJsonAsync("api/todo", model);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<int>();
        }

        public async Task DeleteTodoComment(int? command)
        {
            var client = await _client.DeleteAsync("api/todocomment/" + command.Value);

            client.EnsureSuccessStatusCode();
        }

        public async Task DeleteTodoTag(int? command)
        {
            var client = await _client.DeleteAsync("api/todotag/" + command.Value);

            client.EnsureSuccessStatusCode();
        }

        public async Task<int> EditTodoComment(CreateTodoCommentCommand command)
        {
            var client = await _client.PostAsJsonAsync("api/todocomment", command);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<int>();
        }

        public async Task<int> EditTodoTask(EditTodoCommand model)
        {
            var client = await _client.PostAsJsonAsync("api/todo", model);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<int>();
        }

        public async Task<PagedTodoResultDto> FilterTodos(TodoFilterSearchQuery model)
        {
            var url = CreateUrl("api/todo/filter", model);
            var client = await _client.GetAsync(url);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<PagedTodoResultDto>();
        }

        public async Task<PagedTodoResultDto> SearchTodos(TodoSearchQuery model)
        {
            var url = CreateUrl("api/todo", model);
            var client = await _client.GetAsync(url);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<PagedTodoResultDto>();
        }

        private string CreateUrl<T>(string path, T model)
        {
            var queryStringDict = typeof(T).GetProperties()
                .Select(_ => new { _.Name, Value = _.GetValue(model)?.ToString() })
                .Where(_ => !string.IsNullOrEmpty(_.Value))
                .ToDictionary(_ => _.Name, _ => _.Value);

            return QueryHelpers.AddQueryString(path, queryStringDict);
        }
    }
}