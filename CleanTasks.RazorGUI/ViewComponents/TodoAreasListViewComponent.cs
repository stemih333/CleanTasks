using CleanTasks.Application.TodoArea.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CleanTasks.RazorGUI.Pages.ViewComponents
{
    public class TodoAreasListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<TodoAreaDto> areas)
        {           
            return View(areas);
        }
    }
}
