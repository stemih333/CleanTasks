using CleanTasks.Application.TodoArea.Models;
using CleanTasks.RazorGUI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<List<TodoAreaDto>> GetTodoAreas(List<string> allowedAreas)
        {
            var queryString = TodoAreasQueryString(allowedAreas);
            var client = await _client.GetAsync("api/todoarea" + queryString);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<TodoAreaDto>>();
        }

        public async Task CreateTodoArea(string areaName, string userName)
        {
            var client = await _client.PutAsJsonAsync("api/todoarea", new { Value = areaName, UserName = userName });

            client.EnsureSuccessStatusCode();
        }

        public async Task DeleteTodoArea(int areaId)
        {
            var client = await _client.DeleteAsync("api/todoarea/" + areaId);

            client.EnsureSuccessStatusCode();
        }

        private string TodoAreasQueryString(List<string> allowedAreas)
        {
            var builder = new StringBuilder("?");
            for (var i = 0; i < allowedAreas.Count; i++)
            {
                builder.Append($"allowedAreas[{i}]={allowedAreas[i]}");
                if ((i + 1) < allowedAreas.Count) builder.Append("&");
            }

            return builder.ToString();
        }


    }
}
