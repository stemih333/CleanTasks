using System.Collections.Generic;
using System.Security.Claims;

namespace TodoTasks.OpenIdConnectAuth
{
    public class DevIdentity : ClaimsIdentity
    {
        public DevIdentity(IEnumerable<Claim> claims) : base(claims)
        {}

        public override bool IsAuthenticated => true;
    }
}
