using AuthAPI.CustomAttribute;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

public class PermissionRequirement : IAuthorizationRequirement
{
    public Permissions[] Permissions { get; }
    public PermissionMode Mode { get; }

    public PermissionRequirement(Permissions[] permissions, PermissionMode mode)
    {
        Permissions = permissions;
        Mode = mode;
    }
}
