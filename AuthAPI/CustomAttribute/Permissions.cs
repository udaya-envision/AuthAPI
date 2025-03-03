namespace AuthAPI.CustomAttribute;

public enum Permissions
{
    // Read
    Read = 0,
    None = 1,
    View = 2,
    Search = 3,

    // Write
    Create = 4,
    Edit = 5,
    Update = 6,
    Modify = 7,

    // Delete
    Delete = 8,
    Remove = 9,

    // Management
    Manage = 10,
    Administer = 11,
    Configure = 12,

    // Other
    Export = 13,
    Import = 14,
    Download = 15,
    Upload = 16,
    Approve = 17,
    Reject = 18
}
