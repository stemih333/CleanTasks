using CleanTasks.Application.ReferenceData.Models;
using CleanTasks.Application.ReferenceData.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanTasks.WebAPI.Controllers
{
    public class ReferenceDataController : TodoControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReferenceDataDto>> Get()
        => await Mediator.Send(new ReferenceDataQuery());
    }
}
