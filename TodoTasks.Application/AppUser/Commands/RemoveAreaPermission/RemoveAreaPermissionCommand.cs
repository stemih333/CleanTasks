using MediatR;

namespace TodoTasks.Application.AppUser.Commands
{
    public class RemoveAreaPermissionCommand : IRequest
    {
        public string Username { get; set; }
        public string Permission { get; set; }
    }
}
