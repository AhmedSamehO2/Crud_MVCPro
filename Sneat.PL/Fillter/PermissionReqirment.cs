using Microsoft.AspNetCore.Authorization;

namespace Sneat.PL.Fillter
{
    public class PermissionReqirment : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionReqirment(string permission)
        {
            Permission = permission;
        }
    }
}
