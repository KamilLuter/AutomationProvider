using ErrorOr;
using System;
using System.Linq;
using System.Security.Claims;
using AutomationProvider.Application.Common.Errors; 

namespace AutomationProvider.Api.Common.Utils 
{
    public static class ClaimsUtils
    {
        public static ErrorOr<Guid> GetCustomerId(HttpContext httpContext) 
        {
            var userClaims = httpContext.User.Claims; 
            var nameIdentifierClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null)
            {
                if (Guid.TryParse(nameIdentifierClaim.Value, out Guid customerId)) 
                {
                    return customerId; 
                }
                else
                {
                    return Errors.Authorization.Unauthorized;
                }
            }
            else
            {
                return Errors.Authorization.Unauthorized; 
            }
        }
    }
}
