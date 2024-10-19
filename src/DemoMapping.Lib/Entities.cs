using System.Diagnostics;

namespace DemoMapping.Lib;

public abstract class EntityBase
{
    public int Id { get; set; }
    public DateTimeOffset LastChanged { get; set; }
     public bool IsDeleted { get; set; }
}

[DebuggerDisplay("{Id}, {RoleName}")]
public class RoleEntity : EntityBase
{
    public string RoleName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class UserEntity : EntityBase
{
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool NameVerified { get; set; }
    public string Email { get; set; } = string.Empty;
    public RoleEntity[] Roles { get; set; } = [];
    public bool IsActivated { get; set; }
}