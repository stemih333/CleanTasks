using System.Collections.Generic;

namespace IdentityServer4.Quickstart.UI
{
    public class EditViewModel : ApplicationUserModel
    {
        public Dictionary<string, string> Permissions { get; set; }
    }
}
