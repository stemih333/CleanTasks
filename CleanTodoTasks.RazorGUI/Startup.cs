using CleanTasks.Common.Constants;
using CleanTasks.CommonWeb.Classes;
using CleanTodoTasks.RazorGUI.Interfaces;
using CleanTodoTasks.RazorGUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CleanTodoTasks.RazorGUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IAppSessionHandler, AppSessionHandler>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddHttpContextAccessor();

            services.AddHttpClient<ITodoAreaApiClient, TodoAreaApiClient>(async (c) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                
                c.BaseAddress = new Uri("https://localhost:5004/");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });

            services.AddHttpClient<ITodoApiClient, TodoApiClient>(async (c) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                
                c.BaseAddress = new Uri("https://localhost:5004/");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });

            services.AddHttpClient<IUserApiClient, UserApiClient>(async (c) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");

                c.BaseAddress = new Uri("https://localhost:5000/");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            });


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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookie";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookie", opts => {
                opts.ExpireTimeSpan = TimeSpan.FromHours(1);
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookie";
                options.Authority = "https://localhost:5000";
                options.ClientId = "razorgui_ID";
                options.ClientSecret = "RazorGUISecret";
                options.ResponseType = "code id_token";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("WebAPI");
                options.Scope.Add("offline_access");
                options.Scope.Add(AuthConstants.PermissionType);
                options.Scope.Add(PermissionTypes.TodoAreaPermission);
                options.ClaimActions.Add(new JsonKeyArrayClaimAction(AuthConstants.PermissionType, AuthConstants.PermissionType, AuthConstants.PermissionType));
                options.ClaimActions.Add(new JsonKeyArrayClaimAction(PermissionTypes.TodoAreaPermission, PermissionTypes.TodoAreaPermission, PermissionTypes.TodoAreaPermission));
                options.Events.OnRemoteFailure = ctx =>
                {
                    if(!string.IsNullOrEmpty(ctx.Failure.Message) && ctx.Failure.Message.Contains("access_denied"))
                    {
                        ctx.Response.Redirect("/");
                        ctx.HandleResponse();
                    }
                    return Task.CompletedTask;
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc();
        }
    }

    
}
