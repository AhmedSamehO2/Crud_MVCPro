using Microsoft.AspNetCore.Authorization;

namespace Sneat.PL.Fillter
{
    public class PermissionAuthHandler : AuthorizationHandler<PermissionReqirment>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionReqirment requirement)
        {
            if (context.User is null)
                return;
            var CanAccess = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission && c.Issuer == "LOCAL AUTHORITY");
            if(CanAccess)
            {
                context.Succeed(requirement); 
                return;
            }
        }
    }
}
