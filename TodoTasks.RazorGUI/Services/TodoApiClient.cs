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
using TodoTasks.Application.Attachment.Models;
using System.Collections.Generic;
using TodoTasks.Application.Attachment.Commands;
using System.Net.Http.Headers;
using TodoTasks.Domain.Entities;
using System.Text;
using TodoTasks.Application.ReferenceData.Models;
using TodoTasks.Application.TodoArea.Models;

namespace TodoTasks.RazorGUI.Services {
    public class TodoApiClient : ITodoApiClient {
        private readonly HttpClient _client;

        public TodoApiClient (HttpClient client) {
            _client = client;
        }

        public async Task<int> CreateAttachment(CreateAttachmentCommand command)
        {
            using (var multiContent = new MultipartFormDataContent())
            {
                multiContent.Add(new StreamContent(command.FileStream) { Headers = {
                        ContentLength = command.FileSize,
                        ContentType = new MediaTypeHeaderValue(command.FileType)
                    } },"File", command.FileName);

                if (!string.IsNullOrEmpty(command.Description)) multiContent.Add(new StringContent(command.Description), "Description");
                multiContent.Add(new StringContent(command.UserId), "UserId");
                multiContent.Add(new StringContent(command.TodoId.ToString()), "TodoId");

                using (var result = await _client.PutAsync("api/attachment", multiContent))
                {
                    result.EnsureSuccessStatusCode();

                    return await result.Content.ReadAsAsync<int>();
                }
            }           
        }

        public async Task<int> CreateTodoComment(CreateTodoCommentCommand command)
        {
            using (var result = await _client.PutAsJsonAsync("api/todocomment", command))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<int>();
            }
        }

        public async Task<int> CreateTodoTag(CreateTodoTagCommand command)
        {
            using (var result = await _client.PutAsJsonAsync("api/todotag", command))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<int>();
            }
        }

        public async Task<int> CreateTodoTask(CreateTodoCommand model) {
            using (var result = await _client.PutAsJsonAsync("api/todo", model))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<int>();
            }
        }

        public async Task DeleteAttachment(int? attachmentId)
        {
            using (var result = await _client.DeleteAsync("api/attachment/" + attachmentId.Value))
            {
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteTodoComment(int? command)
        {
            using (var result = await _client.DeleteAsync("api/todocomment/" + command.Value))
            {
                result.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteTodoTag(int? command)
        {
            using (var result = await _client.DeleteAsync("api/todotag/" + command.Value))
            {
                result.EnsureSuccessStatusCode();
            }              
        }

        public async Task<int> EditTodoComment(CreateTodoCommentCommand command)
        {
            using (var result = await _client.PostAsJsonAsync("api/todocomment", command))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<int>();
            }
        }

        public async Task<int> EditTodoTask(EditTodoCommand model)
        {
            using (var result = await _client.PostAsJsonAsync("api/todo", model))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<int>();
            }
        }

        public async Task<PagedTodoResultDto> FilterTodos(TodoFilterSearchQuery model)
        {
            var url = CreateUrl("api/todo/filter", model);
            using (var result = await _client.GetAsync(url))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<PagedTodoResultDto>();
            }
        }

        public async Task<AttachmentDto> GetAttachment(int? attachmentId)
        {
            using (var result = await _client.GetAsync("api/attachment/single/" + attachmentId))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<AttachmentDto>();
            }
        }

        public async Task<IEnumerable<AttachmentDto>> GetAttachments(int? todoId)
        {
            using (var result = await _client.GetAsync("api/attachment/" + todoId))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<IEnumerable<AttachmentDto>>();
            }
        }

        public async Task<PagedTodoResultDto> SearchTodos(TodoSearchQuery model)
        {
            var url = CreateUrl("api/todo", model);
            using (var result = await _client.GetAsync(url))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<PagedTodoResultDto>();
            }
        }

        public async Task<IEnumerable<AppUser>> SearchUsers(string claimType, string claimValue)
        {
            using (var result = await _client.GetAsync($"api/appuser/users?claimtype={claimType}&claimvalue={claimValue}"))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<IEnumerable<AppUser>>();
            }
        }

        public async Task<PermissionUser> GetPermissionUser(string username)
        {
            using (var result = await _client.GetAsync("api/appuser/userpermission?username=" + username))
            {
                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<PermissionUser>();
            }

        }

        public async Task<IEnumerable<TodoAreaDto>> GetAllTodoAreas()
        {
            using (var client = await _client.GetAsync("api/todoarea/all"))
            {
                client.EnsureSuccessStatusCode();

                return await client.Content.ReadAsAsync<IEnumerable<TodoAreaDto>>();
            }
        }

        public async Task<IEnumerable<TodoAreaDto>> GetTodoAreas(IEnumerable<string> allowedAreas)
        {
            var queryString = allowedAreas.Any() ? TodoAreasQueryString(allowedAreas.ToList()) : "";
            using (var client = await _client.GetAsync("api/todoarea" + queryString))
            {
                client.EnsureSuccessStatusCode();
                
                return await client.Content.ReadAsAsync<IEnumerable<TodoAreaDto>>();
            }
        }

        public async Task CreateTodoArea(string areaName, string userName)
        {
            using (var client = await _client.PutAsJsonAsync("api/todoarea", new { Value = areaName, UserName = userName }))
            {
                client.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteTodoArea(int areaId)
        {
            using (var client = await _client.DeleteAsync("api/todoarea/" + areaId))
            {
                client.EnsureSuccessStatusCode();
            }
        }

        public async Task EditTodoArea(string areaName, int areaId, string userName)
        {
            using (var client = await _client.PostAsJsonAsync("api/todoarea", new { Value = areaName, Id = areaId, UserName = userName }))
            {
                client.EnsureSuccessStatusCode();
            }
        }

        public async Task<ReferenceDataDto> GetReferenceData()
        {
            using (var client = await _client.GetAsync("api/referencedata"))
            {
                client.EnsureSuccessStatusCode();

                return await client.Content.ReadAsAsync<ReferenceDataDto>();
            }
        }

        public async Task CreateAreaPermission(int? areaId, string username)
        {
            using (var client = await _client.PutAsJsonAsync("api/appuser/area", new { Permission = areaId, UserName = username }))
            {
                client.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAreaPermission(int? areaId, string username)
        {
            using (var client = await _client.DeleteAsync($"api/appuser?permission={areaId.Value.ToString()}&username={username}"))
            {
                client.EnsureSuccessStatusCode();
            }
        }
        public async Task<bool> AreaExist(string id)
        {
            var areas = await GetTodoAreas(new List<string> { id });
            return areas != null && areas.Any();
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