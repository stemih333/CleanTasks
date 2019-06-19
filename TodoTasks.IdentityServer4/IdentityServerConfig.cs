using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace TodoTasks.IdentityServer4
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = ClaimTypes.Surname,
                    DisplayName = "Surname",
                    Description = "Users surname",
                    UserClaims = new[] { ClaimTypes.Surname }
                },
                new IdentityResource
                {
                    Name = ClaimTypes.GivenName,
                    DisplayName = "Given name",
                    Description = "Users given name",
                    UserClaims = new[] { ClaimTypes.GivenName }
                },
                new IdentityResource
                {
                    Name = ClaimTypes.Email,
                    DisplayName = "E-mail",
                    Description = "Users e-mail address",
                    UserClaims = new[] { ClaimTypes.Email }
                },
                new IdentityResource
                {
                    Name = ClaimTypes.Name,
                    DisplayName = "Name",
                    Description = "Users username",
                    UserClaims = new[] { ClaimTypes.Name }
                },
                new IdentityResource
                {
                    Name = AuthConstants.PermissionType,
                    DisplayName = "Permission type",
                    UserClaims = new[] { AuthConstants.PermissionType }
                }
            };

        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource("WebAPI", "Web API", new[] {
                    ClaimTypes.Surname,
                    ClaimTypes.GivenName,
                    ClaimTypes.Email,
                    ClaimTypes.Name,
                    AuthConstants.PermissionType })
                {
                    Description = "REST API with Todo action endpoints.",
                    Enabled = true,
                    ApiSecrets = new List<Secret> { new Secret("WebAPISecret") }
                }
            };

        public static IEnumerable<Client> GetClients()
            => new List<Client>
            {
                new Client
                {
                    ClientId = "razorgui_ID",
                    ClientName = "RazorGUI",
                    ClientSecrets = new List<Secret> { new Secret("RazorGUISecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RedirectUris = new List<string> { "https://localhost:5002/sign-in-oidc" },
                    BackChannelLogoutUri = "https://localhost:5002/",
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "WebAPI",
                        ClaimTypes.Surname,
                        ClaimTypes.GivenName,
                        ClaimTypes.Email,
                        ClaimTypes.Name,
                        AuthConstants.PermissionType
                    },
                    Description = "MVC Razor Pages GUI.",
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    IdentityTokenLifetime = 3600,
                    AccessTokenLifetime = 3600
                }
            };
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "stefan.mihailovic@if.se",
                    Password = "Sommaren1941!",
                    IsActive = true,
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Surname, "Stefan"),
                        new Claim(ClaimTypes.GivenName, "Mihailovic"),
                        new Claim(ClaimTypes.Email, "stefan.mihailovic@if.se"),
                        new Claim(ClaimTypes.Name, "stefan.mihailovic@if.se"),
                        new Claim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission)
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "stemih11@gmail.com",
                    Password = "Sommaren1984!",
                    IsActive = true,
                    Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Surname, "Stefan"),
                        new Claim(ClaimTypes.GivenName, "Mihailovic"),
                        new Claim(ClaimTypes.Email, "stemih11@gmail.com"),
                        new Claim(ClaimTypes.Name, "stemih11@gmail.com"),
                        new Claim(AuthConstants.PermissionType, AuthConstants.UserPermission)
                    }
                }
            };
        }
    }
}
