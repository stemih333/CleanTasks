using CleanTasks.Application.User.Models;
using MediatR;
using System.Collections.Generic;

namespace CleanTasks.Application.User.Queries
{
    public class GetUsersByAreaQuery : IRequest<List<UserDto>> {
        public int? Id { get; set; }
    }
}
