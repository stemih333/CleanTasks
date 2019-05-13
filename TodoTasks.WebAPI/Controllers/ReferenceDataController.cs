using CleanTodoTasks.Application.ReferenceData.Models;
using CleanTodoTasks.Application.ReferenceData.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanTodoTasks.WebAPI.Controllers
{
    public class ReferenceDataController : TodoControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReferenceDataDto>> Get()
        => await Mediator.Send(new ReferenceDataQuery());
    }
}
