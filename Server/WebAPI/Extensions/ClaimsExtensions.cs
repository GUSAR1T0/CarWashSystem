using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace VXDesign.Store.CarWashSystem.Server.WebAPI.Extensions
{
    internal static class ClaimsExtensions
    {
        internal static string? Get(this IEnumerable<Claim> claims, string name)
        {
            return claims.FirstOrDefault(c => string.Equals(c.Type, name, StringComparison.InvariantCultureIgnoreCase))?.Value;
        }
    }
}