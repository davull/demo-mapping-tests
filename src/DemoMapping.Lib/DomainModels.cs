namespace DemoMapping.Lib;

public class RoleModel
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class NameModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool Verified { get; set; }
}

public class UserModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public NameModel Name { get; set; } = new();
    public RoleModel[] Roles { get; set; } = [];
    public bool IsActivated { get; set; }
}