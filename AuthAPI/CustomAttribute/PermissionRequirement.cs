using AuthAPI.CustomAttribute;
using Microsoft.AspNetCore.Authorization;

namespace AuthAPI.CustomAttribute;

public class PermissionRequirement(Permissions[] permission, PermissionMode mode) : IAuthorizationRequirement
{
    public PermissionMode Mode { get; } = mode;
    public Permissions[] Permissions { get; } = permission;
}