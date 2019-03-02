﻿using CleanTasks.Common.Constants;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace CleanTasks.IdentityServer4
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource(AuthConstants.PermissionType, "Users permissions to application", new []{ AuthConstants.PermissionType })
            };

        public static IEnumerable<ApiResource> GetApiResources()
            => new List<ApiResource>
            {
                new ApiResource("WebAPI", "Web API")
                {
                    Description = "REST API with Todo action endpoints.",
                    Enabled = true
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
                    //AlwaysIncludeUserClaimsInIdToken = false,
                    RedirectUris = new List<string> { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "WebAPI",
                        AuthConstants.PermissionType
                    },
                    Description = "MVC Razor Pages GUI."
                },
                new Client
                {
                    ClientId = "adminGui_ID",
                    ClientName = "AdminGUI",
                    ClientSecrets = new List<Secret> { new Secret("AdminGUISecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    //AlwaysIncludeUserClaimsInIdToken = false,
                    RedirectUris = new List<string> { "https://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:5000/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "WebAPI",
                        AuthConstants.PermissionType
                    },
                    Description = "MVC Admin GUI."
                }
            };

        public static List<TestUser> GetTestUsers()
            => new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mihste",
                    IsActive = true,
                    Password = "Stemih11",
                    Claims = new List<Claim>
                    {
                        new Claim("name", "Stefan Mihailovic"),
                        new Claim("email", "stemih11@gmail.com"),
                        new Claim("nickname", "stemih"),
                        new Claim("test", "test claim")
                    }
                }
            };
    }
}
