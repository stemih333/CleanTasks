using Microsoft.AspNetCore.Identity;

namespace TodoTasks.DataAccess.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
