using System.IO;
using System.Threading.Tasks;

namespace TodoTasks.Application.Interfaces
{
    public interface IFileSaver
    {
        Task SaveFile(string filename, Stream stream);
        Task DeleteFile(string filename);
        Task<string> GetFilePath(string filename);
    }
}
