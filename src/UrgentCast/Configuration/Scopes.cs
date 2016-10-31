using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace UrgentCast.Configuration
{
    public static class Scopes
    {
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "feed",
                    Description = "Feed-related Information",

                    Type = ScopeType.Resource
                }
            };
        }
    }
}