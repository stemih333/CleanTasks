using TodoTasks.Application.ReferenceData.Models;
using TodoTasks.Application.ReferenceData.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TodoTasks.WebAPI.Controllers
{
    public class ReferenceDataController : TodoControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReferenceDataDto>> Get()
        => await Mediator.Send(new ReferenceDataQuery());
    }
}
