using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using TodoTasks.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using TodoTasks.OpenIdConnectAuth.Auth;

namespace TodoTasks.OpenIdConnectAuth
{
    public static class OpenIdConnectStartup
    {
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

                            var userRepo = ctx.HttpContext.RequestServices.GetRequiredService<IAppUserRepository>();
                            if (userRepo == null) throw new NotImplementedException("Could not retrieve service IAppUserRepository.");
                            // Check if user exists in database. Users that dont exist get added automatically to database as regular users.

                            var user = await userRepo.GetUserAsClaimsIdentity(ctx.Principal.Identity.Name);
                            if (user == null) { ctx.Fail($"Principal {ctx.Principal.Identity.Name} is not authorized."); return; }

                            ctx.Principal.AddIdentity(user);
                        }
                    };

                });
            //.AddAzureADBearer(options => configuration.Bind("AuthSettings", options));
        }

        public static void ConfigureOpenIdServices(IServiceCollection services, IConfiguration configuration, bool isIdentityServer = false)
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
                if (isIdentityServer)
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
                        if (string.IsNullOrEmpty(ctx.Principal.Identity.Name)) { ctx.Fail($"Unknown user is trying to access the API."); return; }

                        var userRepo = ctx.HttpContext.RequestServices.GetRequiredService<IAppUserRepository>();
                        if (userRepo == null) throw new Exception("Could not retrieve service IAppUserRepository.");

                        var user = await userRepo.GetUserAsClaimsIdentity(ctx.Principal.Identity.Name);
                        if (user == null) await userRepo.CreateUserFromClaimsPrincipal(ctx.Principal);

                        var appIdentity = await userRepo.GetUserAsClaimsIdentity(ctx.Principal.Identity.Name);
                        if (appIdentity == null) { ctx.Fail($"Failed to authorize user {ctx.Principal.Identity.Name}. User did not get added to app user storage."); return; }
                        ctx.Principal.AddIdentity(appIdentity);
                    }
                };
            }).AddCookie();
        }
    }
}
