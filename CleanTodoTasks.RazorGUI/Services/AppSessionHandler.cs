using CleanTodoTasks.RazorGUI.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CleanTodoTasks.RazorGUI.Services
{
    public class AppSessionHandler : IAppSessionHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AppSessionHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void ClearSession()
        {
            _contextAccessor.HttpContext.Session.Clear();
        }

        public void DeleteData(string key)
        {
            _contextAccessor.HttpContext.Session.Remove(key);
        }

        public T GetData<T>(string key)
        {
            var data = _contextAccessor.HttpContext.Session.GetString(key);

            if(string.IsNullOrEmpty(data)) return default(T);

            return JsonConvert.DeserializeObject<T>(data);
        }

        public void SetData<T>(string key, T data)
        {
            if (data == null) return;
            
            var serializedData = JsonConvert.SerializeObject(data);
            _contextAccessor.HttpContext.Session.SetString(key, serializedData);
        }
    }
}
