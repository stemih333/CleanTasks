using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.AppUser.Commands.SetUserPermission
{
    public class SetUserPermissionHandler : IRequestHandler<SetUserPermissionCommand>
    {
        private readonly IAppUserRepository _appUserRepository;

        public SetUserPermissionHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<Unit> Handle(SetUserPermissionCommand request, CancellationToken cancellationToken)
        {
            await _appUserRepository.SetUserPermission(request.Username, request.Permission);
            return await Unit.Task;
        }           
    }
}
