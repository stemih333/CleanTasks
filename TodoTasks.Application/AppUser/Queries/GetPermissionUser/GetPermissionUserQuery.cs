using MediatR;
using TodoTasks.Domain.Entities;

namespace TodoTasks.Application.AppUser.Queries
{
    public class GetPermissionUserQuery : IRequest<PermissionUser>
    {
        public string Username { get; set; }
    }
}
