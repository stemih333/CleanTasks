using System.Net.Http;
using CleanTasks.RazorGUI.Interfaces;
using System.Threading.Tasks;
using CleanTasks.Application.Todo.Commands;
using CleanTasks.Application.Todo.Models;
using CleanTasks.Application.Todo.Queries;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace CleanTasks.RazorGUI.Services {
    public class TodoApiClient : ITodoApiClient {
        private readonly HttpClient _client;

        public TodoApiClient (HttpClient client) {
            _client = client;
        }

        public async Task CreateTodoTask(CreateTodoCommand model) {
            var client = await _client.PutAsJsonAsync("api/todo", model);

            client.EnsureSuccessStatusCode();
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