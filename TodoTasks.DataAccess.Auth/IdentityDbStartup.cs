using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.DataAccess.Auth
{
    public static class IdentityDbStartup
    {
        public static void ConfigureIdentityServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddIdentityCore<ApplicationUser>(opts => {
                opts.Password.RequiredLength = 8;
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IAppUserRepository, AppUserRepository>();
        }

        public static void ConfigureDevIdentityServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseInMemoryDatabase("TodoDb"));
            services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IAppUserRepository, AppUserRepository>();
        }

        public static void RunIdentityMigrations(IServiceProvider services)
        {
            var context = services.GetService<ApplicationDbContext>();

            context.Database.Migrate();
        }         

        public static async Task SeedIdentityUser(IServiceProvider provider)
        {
            var mngr = provider.GetRequiredService<UserManager<ApplicationUser>>();

            var userEmail = "stemih11@gmail.com";
            var user = await mngr.FindByEmailAsync(userEmail);
            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    FirstName = "Ste",
                    LastName = "Mih"
                };

                var claims = new List<Claim>
                {
                    new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission),
                    new Claim(PermissionTypes.TodoAreaPermission, "1")
                };

                await mngr.CreateAsync(newUser);
                await mngr.AddClaimsAsync(newUser, claims);
            }
        }

        public static async Task SeedIdentityAdmin(IServiceProvider provider)
        {
            var email = Users.Admin.Email;

            var mngr = provider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = await mngr.FindByEmailAsync(email);
            if(admin == null)
            {
                var newAdmin = Users.Admin;

                var claims = Users.AdminClaims;

                await mngr.CreateAsync(newAdmin);
                await mngr.AddClaimsAsync(newAdmin, claims);
            }
        }
    }
}
