using System;
using System.Security.Claims;

namespace WebApp.Controllers.Helpers
{
    public static class IdentityExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            foreach (var claim in user.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    return Guid.Parse(claim.Value);
                }
            }

            return null;
        }
    }
}