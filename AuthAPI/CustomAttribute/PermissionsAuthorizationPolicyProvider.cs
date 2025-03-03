using AuthAPI.CustomAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return _fallbackPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return _fallbackPolicyProvider.GetFallbackPolicyAsync();
    }

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("PermissionPolicy?"))
        {
            var query = policyName.Replace("PermissionPolicy?", "").Split('&');
            var permissions = query[0].Replace("Permissions=", "").Split(',')
                .Select(p => Enum.Parse<Permissions>(p))
                .ToArray();
            var mode = Enum.Parse<PermissionMode>(query[1].Replace("mode=", ""));

            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(permissions, mode))
                .Build();

            return Task.FromResult(policy);
        }

        return _fallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}
