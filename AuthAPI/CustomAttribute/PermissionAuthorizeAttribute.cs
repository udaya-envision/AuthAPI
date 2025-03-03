using Microsoft.AspNetCore.Authorization;

namespace AuthAPI.CustomAttribute;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class PermissionAuthorizeAttribute : AuthorizeAttribute
{
    public PermissionAuthorizeAttribute(Permissions[]? permissions = null, PermissionMode mode = PermissionMode.Any)
    {
        permissions ??= Array.Empty<Permissions>();
        Policy = $"PermissionPolicy?Permissions={string.Join(",", permissions)}&mode={mode}";
    }
}