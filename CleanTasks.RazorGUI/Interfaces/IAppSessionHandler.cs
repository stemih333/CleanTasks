namespace CleanTasks.RazorGUI.Interfaces
{
    public interface IAppSessionHandler
    {
        T GetData<T>(string key);
        void SetData<T>(string key, T data);
        void DeleteData(string key);
        void ClearSession();
    }
}
