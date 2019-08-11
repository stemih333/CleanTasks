using System.Collections.Generic;

namespace TodoTasks.Application.Comparers
{
    public class AppUserComparer : IEqualityComparer<Domain.Entities.AppUser>
    {
        public bool Equals(Domain.Entities.AppUser x, Domain.Entities.AppUser y)
        => x.Id.Equals(y.Id);

        public int GetHashCode(Domain.Entities.AppUser obj)
        => obj.Id.GetHashCode();
    }
}
