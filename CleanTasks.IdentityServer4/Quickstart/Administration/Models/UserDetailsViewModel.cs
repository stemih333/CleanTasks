using System.Collections.Generic;

namespace IdentityServer4.Quickstart.UI
{
    public class UserDetailsViewModel : ApplicationUserModel
    {
        public IEnumerable<string> Permissions { get; set; }
        public IEnumerable<string> Areas { get; set; }
    }
}
