using System.Linq;
using AuthAPI.CustomAttribute;
using Microsoft.AspNetCore.Authorization;

namespace AuthAPI.CustomAttribute;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
    AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var userPermissions = context.User.Claims
            .Where(c => c.Type.Equals("Permissions", StringComparison.OrdinalIgnoreCase))
            .Select(c => c.Value)
            .ToList();

        Console.WriteLine($"User Permissions: {string.Join(", ", userPermissions)}");
        Console.WriteLine($"Required Permissions: {string.Join(", ", requirement.Permissions)}");

        // ✅ Check if the user has at least one required permission
        if (requirement.Permissions.Any(rp => userPermissions.Contains(rp.ToString())))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    protected virtual bool HasAllPermissions(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        return requirement.Mode == PermissionMode.All
            ? requirement.Permissions.All(permission => context.User.HasClaim(c => c.Type == "Permission" && c.Value == permission.ToString()))
            : requirement.Permissions.Any(permission => context.User.HasClaim(c => c.Type == "Permission" && c.Value == permission.ToString()));
    }
}