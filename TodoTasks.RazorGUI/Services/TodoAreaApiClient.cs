using TodoTasks.Application.ReferenceData.Models;
using TodoTasks.Application.TodoArea.Models;
using TodoTasks.Application.TodoAreaPermissions.Models;
using TodoTasks.RazorGUI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks.RazorGUI.Services
{
    public class TodoAreaApiClient : ITodoAreaApiClient
    {
        private readonly HttpClient _client;

        public TodoAreaApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<TodoAreaDto>> GetAllTodoAreas()
        {
            var client = await _client.GetAsync("api/todoarea/all");

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<TodoAreaDto>>();
        }

        public async Task<List<TodoAreaDto>> GetTodoAreas(IEnumerable<string> allowedAreas)
        {
            var queryString = TodoAreasQueryString(allowedAreas.ToList());
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

        public async Task<bool> AreaExist(string id)
        {
            var areas = await GetTodoAreas(new List<string> { id });
            return areas != null && areas.Any();
        }

        public async Task EditTodoArea(string areaName, int areaId, string userName)
        {
            var client = await _client.PostAsJsonAsync("api/todoarea", new { Value = areaName, Id = areaId, UserName = userName });

            client.EnsureSuccessStatusCode();
        }

        public async Task<ReferenceDataDto> GetReferenceData()
        {
            var client = await _client.GetAsync("api/referencedata");

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<ReferenceDataDto>();
        }

        public async Task<List<TodoAreaPermissionDto>> GetAllPermissions()
        {
            var client = await _client.GetAsync("api/todoareapermission");

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<TodoAreaPermissionDto>>();
        }

        public async Task<List<TodoAreaPermissionDto>> GetPermissionsByUserId(string userId)
        {
            var client = await _client.GetAsync("api/todoareapermission?UserId=" + userId);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<TodoAreaPermissionDto>>();
        }

        public async Task<List<TodoAreaPermissionDto>> GetPermissionsByAreaId(int? areaId)
        {
            var client = await _client.GetAsync("api/todoareapermission?TodoAreaId=" + areaId);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<TodoAreaPermissionDto>>();
        }

        public async Task CreateAreaPermission(int? areaId, string userId, string createdBy)
        {
            var client = await _client.PutAsJsonAsync("api/todoareapermission", new { TodoAreaId = areaId, UserId = userId, UserName = createdBy });

            client.EnsureSuccessStatusCode();
        }

        public async Task DeleteAreaPermission(int? permissionId)
        {
            var client = await _client.DeleteAsync("api/todoareapermission/" + permissionId);

            client.EnsureSuccessStatusCode();
        }
    }
}
