using CleanTasks.Application.TodoArea.Models;
using CleanTasks.RazorGUI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Services
{
    public class TodoApiClient : ITodoApiClient
    {
        private readonly HttpClient _client;

        public TodoApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<TodoAreaDto>> GetAllTodoAreas()
        {
            var client = await _client.GetAsync("api/todoarea/all");

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<TodoAreaDto>>();
        }

        public async Task<List<TodoAreaDto>> GetTodoAreas()
        {
            var client = await _client.GetAsync("api/todoarea");

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<TodoAreaDto>>();
        }
    }
}
