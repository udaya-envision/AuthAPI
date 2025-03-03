using AuthAPI.CustomAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        // Retrieve the user's permissions from the claims
        var userPermissions = context.User.FindAll("Permission").Select(c => c.Value).ToList();

        // Check if the user has all required permissions based on the mode
        var hasPermission = requirement.Mode switch
        {
            PermissionMode.All => requirement.Permissions.All(p => userPermissions.Contains(p.ToString())),
            PermissionMode.Any => requirement.Permissions.Any(p => userPermissions.Contains(p.ToString())),
            _ => false
        };

        if (hasPermission)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

