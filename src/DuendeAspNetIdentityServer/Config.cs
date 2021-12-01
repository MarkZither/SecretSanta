// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace DuendeAspNetIdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
                new ApiScope("api1"),
            };
        //https://www.scottbrady91.com/identity-server/aspnet-core-swagger-ui-authorization-using-identityserver4
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44305/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2", "api1" }
                },
                new Client
                {
                    ClientId = "demo_api_swagger",
                    ClientName = "Swagger UI for demo_api",
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = {"https://localhost:44305/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:44305"},
                    AllowedScopes = {"api1"}
                },
                new Client
                {
                    ClientId = "demo_api_client",
                    ClientName = "Web requests for demo_api",
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = false,
                    RequireClientSecret = false,

                    RedirectUris = {"https://localhost:44305/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:44305/signout-callback-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44305/signout-oidc",
                    AllowedCorsOrigins = {"https://localhost:44305"},
                    AllowedScopes = { "openid", "profile", "email", "phone" }    
                },
                new Client
                {
                    ClientId = "demo_native_client",
                    ClientName = "Web requests for demo_api",
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = false,

                    RedirectUris = {"markzithersecretsanta://callback"},
                    PostLogoutRedirectUris = { "markzithersecretsanta://unauthorized" },
                    FrontChannelLogoutUri = "https://localhost:44305/signout-oidc",
                    AllowedCorsOrigins = {"https://localhost:44305"},
                    AllowedScopes = { "openid", "profile", "email", "phone" }
                },
            };
    }
}