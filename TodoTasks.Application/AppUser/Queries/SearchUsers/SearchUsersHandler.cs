using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.Application.AppUser.Queries
{
    public class SearchUsersHandler : IRequestHandler<SearchUsersQuery, IEnumerable<Domain.Entities.AppUser>>
    {
        private readonly IAppUserRepository _appUserRepository;

        public SearchUsersHandler(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<IEnumerable<Domain.Entities.AppUser>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)      
        => string.IsNullOrEmpty(request.ClaimType) && string.IsNullOrEmpty(request.ClaimValue)
            ? _appUserRepository.GetUsers() 
            : await _appUserRepository.SearchUsersByClaim(request.ClaimType, request.ClaimValue);         
        
        
    }
}
