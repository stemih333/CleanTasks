using CleanTasks.Application.User.Models;
using CleanTasks.Application.User.Queries;
using CleanTasks.IdentityServer4.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanTasks.IdentityServer4.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(AuthenticationSchemes = "Bearer,Identity.Application")]
    public class UserController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;

        public UserController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<List<UserDto>> Get() => await _mediator.Send(new GetAllUsersQuery());

        [HttpGet("{id:int?}")]
        public async Task<List<UserDto>> Get(int? id) => await _mediator.Send(new GetUsersByAreaQuery { Id = id });
    }
}
