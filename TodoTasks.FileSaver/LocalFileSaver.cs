using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.FileSaver
{
    public class LocalFileSaver : IFileSaver
    {
        private readonly string _filePath;
        public LocalFileSaver(IHostingEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "Files");
        }

        public async Task SaveFile(string filename, Stream stream)
        {
            var fullPath = Path.Combine(_filePath, filename);
            using(var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

        public Task<string> GetFilePath(string filename) => Task.FromResult("https://localhost:5004/attachments/" + filename);

        public Task DeleteFile(string filename)
        {
            var path = Path.Combine(_filePath, filename);
            File.Delete(path);
            return Task.CompletedTask;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileSaver, LocalFileSaver>();
        }

        public static StaticFileOptions GetStaticFileOptions(IHostingEnvironment env) => new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Files")),
            ServeUnknownFileTypes = true, RequestPath = "/attachments"
        };
    }
}
