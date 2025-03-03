namespace AuthAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }

    public class Role
    {
        public string Name { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }

    public class RolePermission
    {
        public Permission Permission { get; set; }
    }

    public class Permission
    {
        public string Name { get; set; }
    }
}
