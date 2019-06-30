using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.FileSaver
{
    public class FileSaver : IFileSaver
    {
        public async Task SaveFile(string path, byte[] fileBytes)
        {
            await File.WriteAllBytesAsync(path, fileBytes);
        }

        public Task DeleteFile(string path)
        {
            File.Delete(path);
            return Task.CompletedTask;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileSaver, FileSaver>();
        }
    }
}
