using CleanTasks.Application.User.Models;
using CleanTasks.RazorGUI.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _client;

        public UserApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var client = await _client.GetAsync("api/user");

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<UserDto>>();
        }

        public async Task<List<UserDto>> GetUsersByArea(int id)
        {
            var client = await _client.GetAsync("api/user/" + id);

            client.EnsureSuccessStatusCode();

            return await client.Content.ReadAsAsync<List<UserDto>>();
        }
    }
}
