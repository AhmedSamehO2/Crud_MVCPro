using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Sneat.PL.Fillter
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallBackPolicyProvider { get; set; }
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> option)
        {
            FallBackPolicyProvider = new DefaultAuthorizationPolicyProvider(option);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return FallBackPolicyProvider.GetDefaultPolicyAsync();

        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionReqirment(policyName));
                return Task.FromResult(policy.Build());
            }
            return FallBackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
