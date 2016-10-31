using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace UrgentCast.Configuration
{
    public static class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "feedrefresher",

                    // No interactive user, use clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // Secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // Scopes the client has access to
                    AllowedScopes =
                    {
                        "feed"
                    }
                }
            };
        }
    }
}