using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks.DataAccess.Auth
{
    public class AuthStartup
    {
        public static void ConfigureIdentity(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddIdentityCore<IdentityUser>(opts => {
                opts.Password.RequiredLength = 8;
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Admin, policy =>
                    policy.RequireClaim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission));
                options.AddPolicy(Policies.User, policy =>
                    policy.RequireClaim(AuthConstants.PermissionType, AuthConstants.UserPermission));
                options.AddPolicy(Policies.All, policy =>
                    policy.RequireAssertion(
                        assert =>
                            assert.User.HasClaim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission) ||
                            assert.User.HasClaim(AuthConstants.PermissionType, AuthConstants.UserPermission)));
            });
        }

        public static void ConfigureOpenIdApi(IServiceCollection services, IConfiguration configuration)
        {
            var config = new AuthSettings();
            configuration.Bind("AuthSettings", config);
            services.AddSingleton(config);

            services.AddAuthentication(opts => opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.Authority = string.IsNullOrEmpty(config.TenantId) ? config.Instance : new Uri(new Uri(config.Instance), config.TenantId).ToString();
                    opts.Audience = config.ClientId;
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.ClientSecret))
                    };
                });
                //.AddAzureADBearer(options => configuration.Bind("AuthSettings", options));
        }

        public static void ConfigureOpenIdGui(IServiceCollection services, IConfiguration configuration)
        {
            var config = new AuthSettings();
            configuration.Bind("AuthSettings", config);
            services.AddSingleton(config);

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
                options.Authority = $"{config.Instance}{config.Domain}";
                options.ClientId = config.ClientId;
                options.ClientSecret = config.ClientSecret;
                options.ResponseType = config.Type;
                options.CallbackPath = config.CallbackPath;
                options.SignedOutRedirectUri = config.RedirectUrl;
                options.Resource = config.ClientId;             
                options.SaveTokens = true;

                var type = config.AuthType?.Equals("IdentityServer4");
                if (type.HasValue && type.Value)
                {
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add(ClaimTypes.Surname);
                    options.Scope.Add(ClaimTypes.Surname);
                    options.Scope.Add(ClaimTypes.GivenName);
                    options.Scope.Add(ClaimTypes.Email);
                    options.Scope.Add(ClaimTypes.Name);
                    options.Scope.Add("WebAPI");
                    options.Scope.Add("offline_access");
                }

                options.Events.OnTokenValidated = async ctx =>
                {

                    var userManager = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                    // Check if user exists in database. Users that dont exist get added automatically to database as regular users.
                    var user = await userManager.FindByEmailAsync(ctx.Principal.Identity.Name) ?? await userManager.FindByNameAsync(ctx.Principal.Identity.Name);
                    IList<Claim> claimsToAdd = new List<Claim>();
                    if (user == null)
                    {
                        var name = ctx.Principal.Claims.First(_ => _.Type.Equals(ClaimTypes.Name));
                        var givenName = ctx.Principal.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.GivenName));
                        var surname = ctx.Principal.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.Surname));
                        if (name == null || givenName == null || surname == null) throw new Exception($"User {ctx.Principal.Identity.Name ?? "Unknown"} is missing one or more name claims.");
                        user = new IdentityUser
                        {
                            Email = ctx.Principal.Identity.Name,
                            UserName = name.Value
                        };

                        var userResult = await userManager.CreateAsync(user);
                        if (!userResult.Succeeded) throw new Exception("Failed to create user: " + ctx.Principal.Identity.Name);

                        claimsToAdd = new List<Claim> { name, givenName, surname, new Claim(ClaimTypes.Email, ctx.Principal.Identity.Name) };

                        var result = await userManager.AddClaimsAsync(user, claimsToAdd);
                        if (!result.Succeeded) throw new Exception("Failed to add claims for user: " + ctx.Principal.Identity.Name);
                    }
                    // Append the claims retrieved from database to user logged in with Open ID.
                    if (!claimsToAdd.Any()) claimsToAdd = await userManager.GetClaimsAsync(user);
                    var appIdentity = new ClaimsIdentity(claimsToAdd);
                    ctx.Principal.AddIdentity(appIdentity);
                };
            }).AddCookie();
        }

        public static async Task SeedAsync(IServiceProvider provider)
        {
            var email = "stefan.mihailovic@if.se";
            var firstName = "Stefan";
            var lastName = "Mihailovic";

            var mngr = provider.GetRequiredService<UserManager<IdentityUser>>();

            var admin = await mngr.FindByEmailAsync(email);
            if(admin == null)
            {
                var newAdmin = new IdentityUser
                {
                    Email = email,
                    UserName = email
                };

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.GivenName, firstName),
                    new Claim(ClaimTypes.Surname, lastName),
                    new Claim(ClaimTypes.Name, email),
                    new Claim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission)
                };

                await mngr.CreateAsync(newAdmin);
                await mngr.AddClaimsAsync(newAdmin, claims);
            }
        }
    }
}
