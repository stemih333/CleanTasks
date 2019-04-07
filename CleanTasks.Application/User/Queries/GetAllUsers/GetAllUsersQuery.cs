using CleanTasks.Application.User.Models;
using MediatR;
using System.Collections.Generic;

namespace CleanTasks.Application.User.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>> {}
}
