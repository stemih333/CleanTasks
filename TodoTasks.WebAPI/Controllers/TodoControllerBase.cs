using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TodoTasks.DataAccess.Auth;

namespace TodoTasks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Policy = Policies.User)]
    public abstract class TodoControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }


}