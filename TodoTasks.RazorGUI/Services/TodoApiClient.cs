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
                multiContent.Add(new ByteArrayContent(command.FileBytes) {
                    Headers = {
                        ContentLength = command.FileSize,
                        ContentType = new MediaTypeHeaderValue(command.FileType)
                    }
                }, "File", command.FileName);
                if (!string.IsNullOrEmpty(command.Description)) multiContent.Add(new StringContent(command.Description), "Description");
                multiContent.Add(new StringContent(command.UserId), "UserId");
                multiContent.Add(new StringContent(command.TodoId.ToString()), "TodoId");

                var result = await _client.PutAsync("api/attachment", multiContent);

                result.EnsureSuccessStatusCode();

                return await result.Content.ReadAsAsync<int>();
            }           
        }

        public async Task<int> CreateTodoComment(CreateTodoCommentCommand command)
        {
            var result = await _client.PutAsJsonAsync("api/todocomment", command);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<int>();
        }

        public async Task<int> CreateTodoTag(CreateTodoTagCommand command)
        {
            var result = await _client.PutAsJsonAsync("api/todotag", command);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<int>();
        }

        public async Task<int> CreateTodoTask(CreateTodoCommand model) {
            var result = await _client.PutAsJsonAsync("api/todo", model);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<int>();
        }

        public async Task DeleteAttachment(int? attachmentId)
        {
            var result = await _client.DeleteAsync("api/attachment/" + attachmentId.Value);

            result.EnsureSuccessStatusCode();
        }

        public async Task DeleteTodoComment(int? command)
        {
            var result = await _client.DeleteAsync("api/todocomment/" + command.Value);

            result.EnsureSuccessStatusCode();
        }

        public async Task DeleteTodoTag(int? command)
        {
            var result = await _client.DeleteAsync("api/todotag/" + command.Value);

            result.EnsureSuccessStatusCode();
        }

        public async Task<int> EditTodoComment(CreateTodoCommentCommand command)
        {
            var result = await _client.PostAsJsonAsync("api/todocomment", command);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<int>();
        }

        public async Task<int> EditTodoTask(EditTodoCommand model)
        {
            var result = await _client.PostAsJsonAsync("api/todo", model);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<int>();
        }

        public async Task<PagedTodoResultDto> FilterTodos(TodoFilterSearchQuery model)
        {
            var url = CreateUrl("api/todo/filter", model);
            var result = await _client.GetAsync(url);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<PagedTodoResultDto>();
        }

        public async Task<BinaryAttachmentDto> GetAttachment(int? attachmentId)
        {
            var result = await _client.GetAsync("api/attachment/single/" + attachmentId);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<BinaryAttachmentDto>();
        }

        public async Task<IEnumerable<AttachmentDto>> GetAttachments(int? todoId)
        {
            var result = await _client.GetAsync("api/attachment/" + todoId);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<IEnumerable<AttachmentDto>>();
        }

        public async Task<PagedTodoResultDto> SearchTodos(TodoSearchQuery model)
        {
            var url = CreateUrl("api/todo", model);
            var result = await _client.GetAsync(url);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<PagedTodoResultDto>();
        }

        public async Task<IEnumerable<AppUser>> SearchUsers(string claimType, string claimValue)
        {
            var result = await _client.GetAsync($"api/appuser/users?claimtype={claimType}&claimvalue={claimValue}");

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<IEnumerable<AppUser>>();
        }

        public async Task<PermissionUser> GetPermissionUser(string username)
        {
            var result = await _client.GetAsync("api/appuser/userpermission?username=" + username);

            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<PermissionUser>();
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