using MediatR;
using System.Collections.Generic;

namespace TodoTasks.Application.AppUser.Queries
{
    public class SearchUsersQuery : IRequest<IEnumerable<Domain.Entities.AppUser>>
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
