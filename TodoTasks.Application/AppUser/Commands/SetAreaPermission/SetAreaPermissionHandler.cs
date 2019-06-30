using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.AppUser.Commands
{
    public class SetAreaPermissionHandler : IRequestHandler<SetAreaPermissionCommand>
    {
        private readonly IAppUserRepository _appUserRepository;

        public SetAreaPermissionHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<Unit> Handle(SetAreaPermissionCommand request, CancellationToken cancellationToken)
        {
            await _appUserRepository.SetAreaPermission(request.Username, request.Permission);
            return await Unit.Task;
        }           
    }
}
