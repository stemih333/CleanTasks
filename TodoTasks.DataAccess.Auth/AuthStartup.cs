using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.DataAccess.Auth
{
    public static class AuthStartup
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
            services.AddIdentityCore<ApplicationUser>(opts => {
                opts.Password.RequiredLength = 8;
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IAppUserRepository, AppUserRepository>();
        }

        public static void RunIdentityMigrations(IServiceProvider services)
        {
            var context = services.GetService<ApplicationDbContext>();

            context.Database.Migrate();
        }

        public static bool DatabaseExists(IServiceProvider services)
        {
            var context = services.GetService<ApplicationDbContext>();

            return (context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
        }

        public static void ConfigureAuthorizationServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Admin, policy =>
                    policy.RequireClaim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission));
                options.AddPolicy(Policies.User, policy =>
                    policy.RequireClaim(AuthConstants.PermissionType, AuthConstants.UserPermission));
            });
        }

        public static void ConfigureJwtApiServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opts => opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.Authority = string.IsNullOrEmpty(configuration["TenantId"]) ? configuration["Instance"] : new Uri(new Uri(configuration["Instance"]), configuration["TenantId"]).ToString();
                    opts.Audience = configuration["ClientId"];
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["ClientSecret"]))
                    };

                    opts.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async ctx =>
                        {
                            if (string.IsNullOrEmpty(ctx.Principal.Identity.Name)) { ctx.Fail($"Unknown user is trying to access the API."); return; }


                            var userManager = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                            // Check if user exists in database. Users that dont exist get added automatically to database as regular users.
                            var user = await userManager.FindByEmailAsync(ctx.Principal.Identity.Name) ?? await userManager.FindByNameAsync(ctx.Principal.Identity.Name);
                            IList<Claim> claimsToAdd = new List<Claim>();

                            if (user == null) { ctx.Fail($"Principal {ctx.Principal.Identity.Name} is not authorized."); return; }

                            // Append the claims retrieved from database to user logged in with Open ID.
                            if (!claimsToAdd.Any()) claimsToAdd = await userManager.GetClaimsAsync(user);
                            var appIdentity = new ClaimsIdentity(claimsToAdd);
                            ctx.Principal.AddIdentity(appIdentity);
                        }
                    };
                 
                });
                //.AddAzureADBearer(options => configuration.Bind("AuthSettings", options));
        }

        public static void ConfigureOpenIdServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = $"{configuration["Instance"]}{configuration["Domain"]}";
                options.ClientId = configuration["ClientId"];
                options.ClientSecret = configuration["ClientSecret"];
                options.ResponseType = configuration["Type"];
                options.CallbackPath = configuration["CallbackPath"];
                options.SignedOutRedirectUri = configuration["RedirectUrl"];
                options.Resource = configuration["ClientId"];             
                options.SaveTokens = true;

                // IdentityServer4 specific settings
                var type = configuration["AuthType"]?.Equals("IdentityServer4");
                if (type.HasValue && type.Value)
                {
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add(ClaimTypes.Surname);
                    options.Scope.Add(ClaimTypes.GivenName);
                    options.Scope.Add(ClaimTypes.Email);
                    options.Scope.Add(ClaimTypes.Name);
                    options.Scope.Add("WebAPI");
                    options.Scope.Add("offline_access");
                }

                options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated = async ctx =>
                    {

                        var userManager = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                        // Check if user exists in database. Users that dont exist get added automatically to database as regular users.
                        var user = await userManager.FindByEmailAsync(ctx.Principal.Identity.Name) ?? await userManager.FindByNameAsync(ctx.Principal.Identity.Name);

                        IList<Claim> claimsToAdd = new List<Claim>();
                        if (user == null)
                        {
                            var name = ctx.Principal.Claims.First(_ => _.Type.Equals(ClaimTypes.Name));
                            var givenName = ctx.Principal.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.GivenName));
                            var surname = ctx.Principal.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.Surname));
                            if (name == null || givenName == null || surname == null) throw new Exception($"User {ctx.Principal.Identity.Name ?? "Unknown"} is missing one or more name claims.");
                            user = new ApplicationUser
                            {
                                Email = ctx.Principal.Identity.Name,
                                UserName = name.Value,
                                FirstName = givenName.Value,
                                LastName = surname.Value
                            };

                            var userResult = await userManager.CreateAsync(user);
                            if (!userResult.Succeeded) throw new Exception("Failed to create user: " + ctx.Principal.Identity.Name);

                            claimsToAdd = new List<Claim> { new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission) };

                            var result = await userManager.AddClaimsAsync(user, claimsToAdd);
                            if (!result.Succeeded) throw new Exception("Failed to add claims for user: " + ctx.Principal.Identity.Name);
                        }
                        // Append the claims retrieved from database to user logged in with Open ID.
                        if (!claimsToAdd.Any()) claimsToAdd = await userManager.GetClaimsAsync(user);
                        var appIdentity = new ClaimsIdentity(claimsToAdd);
                        ctx.Principal.AddIdentity(appIdentity);
                    }
                };
            }).AddCookie();
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
            var email = "stefan.mihailovic@if.se";
            var firstName = "Stefan";
            var lastName = "Mihailovic";

            var mngr = provider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = await mngr.FindByEmailAsync(email);
            if(admin == null)
            {
                var newAdmin = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                var claims = new List<Claim>
                {
                    new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission),
                    new Claim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission),
                    new Claim(PermissionTypes.TodoAreaPermission, "1"),
                    new Claim(PermissionTypes.TodoAreaPermission, "2"),
                };

                await mngr.CreateAsync(newAdmin);
                await mngr.AddClaimsAsync(newAdmin, claims);
            }
        }
    }
}
