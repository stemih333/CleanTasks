using CleanTasks.Application.Todo.Models;
using CleanTasks.RazorGUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CleanTasks.RazorGUI.ViewComponents
{
    public class TodoListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int areaId, string areaName, List<TodoDto> todos)
        {
            var model = new TodoListComponentModel {
                TodoAreaId = areaId,
                TodoAreaName = areaName,
                Todos = todos
            };

            return View(model);
        }
    }
}
