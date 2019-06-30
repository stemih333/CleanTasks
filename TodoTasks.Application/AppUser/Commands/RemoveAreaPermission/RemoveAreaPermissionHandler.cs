using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.AppUser.Commands
{
    public class RemoveAreaPermissionHandler : IRequestHandler<RemoveAreaPermissionCommand>
    {
        private readonly IAppUserRepository _appUserRepository;

        public RemoveAreaPermissionHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<Unit> Handle(RemoveAreaPermissionCommand request, CancellationToken cancellationToken)
        {
            await _appUserRepository.RemoveAreaPermission(request.Username, request.Permission);
            return await Unit.Task;
        }           
    }
}
