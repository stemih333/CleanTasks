using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;
using TodoTasks.Domain.Entities;

namespace TodoTasks.Application.AppUser.Queries
{
    public class GetPermissionUserHandler : IRequestHandler<GetPermissionUserQuery, PermissionUser>
    {
        private readonly IAppUserRepository _appUserRepository;

        public GetPermissionUserHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public Task<PermissionUser> Handle(GetPermissionUserQuery request, CancellationToken cancellationToken)
        => _appUserRepository.GetUser(request.Username);
    }
}
