using CleanTasks.Application.Interfaces;
using CleanTasks.Application.User.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanTasks.Application.User.Queries
{
    public class GetUsersByQueryHandler : IRequestHandler<GetUsersByAreaQuery, List<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUsersByQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserDto>> Handle(GetUsersByAreaQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsersByArea(request.Id.Value);

            return users;
        }
    }
}
