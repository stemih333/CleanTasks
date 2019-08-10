using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.FileSaver
{
    public class LocalFileSaver : IFileSaver
    {
        public async Task SaveFile(string filename, Stream stream)
        {
            var fullPath = Path.Combine("C:\\Files", filename);
            using(var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

        public Task<string> GetFilePath(string filename) => Task.FromResult("https://localhost:5004/attachments/" + filename);

        public Task DeleteFile(string filename)
        {
            File.Delete(Path.Combine("C:\\Files", filename));
            return Task.CompletedTask;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileSaver, LocalFileSaver>();
        }

        public static StaticFileOptions GetStaticFileOptions() => new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine("C:\\Files")),
            ServeUnknownFileTypes = true, RequestPath = "/attachments"
        };
    }
}
