using System.Threading.Tasks;

namespace TodoTasks.Application.Interfaces
{
    public interface IFileSaver
    {
        Task SaveFile(string path, byte[] fileBytes);
        Task DeleteFile(string path);
    }
}
