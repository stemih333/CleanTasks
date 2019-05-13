using Microsoft.AspNetCore.Mvc;

namespace CleanTodoTasks.RazorGUI.ViewComponents
{
    public class IconSortViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string propertyName, string sortColumn, string sortOrder)
        {
            return View((propertyName, sortColumn, sortOrder));
        }
    }
}
