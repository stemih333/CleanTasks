using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoTasks.Application.AppUser.Commands;
using TodoTasks.Application.AppUser.Queries;
using TodoTasks.Domain.Entities;

namespace TodoTasks.WebAPI.Controllers
{
    public class AppUserController : TodoControllerBase
    {
        [HttpGet("users")]
        public async Task<IEnumerable<AppUser>> SearchUsers([FromQuery]string claimType, [FromQuery]string claimValue)
            => await Mediator.Send(new SearchUsersQuery { ClaimType = claimType, ClaimValue = claimValue });

        [HttpGet("userpermission")]
        public async Task<PermissionUser> GetPermissionUser([FromQuery]string username)
            => await Mediator.Send(new GetPermissionUserQuery { Username = username });

        [HttpPut("area")]
        public async Task AddAreaPermission([FromBody]SetAreaPermissionCommand command) => await Mediator.Send(command);


        [HttpPut]
        public async Task AddUserPermission([FromBody]SetUserPermissionCommand command) => await Mediator.Send(command);

        [HttpDelete]
        public async Task DeleteAreaPermission([FromQuery]RemoveAreaPermissionCommand command) => await Mediator.Send(command);

    }
}
