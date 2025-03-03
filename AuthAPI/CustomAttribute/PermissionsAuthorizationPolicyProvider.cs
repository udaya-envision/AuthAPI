using AuthAPI.CustomAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AuthAPI.CustomAttribute
{
    public class PermissionsAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : DefaultAuthorizationPolicyProvider(options)
    {
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith("PermissionPolicy", StringComparison.OrdinalIgnoreCase))
                return await base.GetPolicyAsync(policyName);

            var policy = new AuthorizationPolicyBuilder();
            var policyValues = policyName.Split("?");
            var permissions = policyValues[1].Split("&").First(x => x.StartsWith("Permissions", StringComparison.OrdinalIgnoreCase)).Split("=")[1].Split(",");
            var mode = policyValues[1].Split("&").First(x => x.StartsWith("mode", StringComparison.OrdinalIgnoreCase)).Split("=")[1];
            policy.AddRequirements(new PermissionRequirement(permissions.Select(Enum.Parse<Permissions>).ToArray(), Enum.Parse<PermissionMode>(mode)));
            return policy.Build();
        }
    }
}