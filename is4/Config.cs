using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4_JwtBearerClientAuth_Example
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "client_using_jwt",
                    ClientName = "Client App using JWT Bearer Token for Client Authentication",

                    ClientSecrets =
                    {
                        new Secret
                        {
                            Type = IdentityServerConstants.SecretTypes.X509CertificateBase64,
                            Value = ""
                        }
                    },

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = {"http://localhost:5001/signin-oidc"},
                    AllowedScopes = {"openid", "profile", "api1"}
                }
            };
        }
    }
}