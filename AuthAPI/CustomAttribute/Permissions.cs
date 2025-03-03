namespace AuthAPI.CustomAttribute;

public enum Permissions
{
    // Read
    Read,
    View,
    Search,

    // Write
    Create,
    Edit,
    Update,
    Modify,

    // Delete
    Delete,
    Remove,

    // Management
    Manage,
    Administer,
    Configure,

    // Other
    Export,
    Import,
    Download,
    Upload,
    Approve,
    Reject
}