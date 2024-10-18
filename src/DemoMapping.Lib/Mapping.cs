namespace DemoMapping.Lib;

public static class Mapping
{
    public static RoleModel ToDomainModel(this RoleEntity entity)
    {
        return new RoleModel
        {
            Id = entity.Id,
            RoleName = entity.RoleName,
            Description = entity.Description
        };
    }

    public static RoleEntity ToEntity(this RoleModel model)
    {
        return new RoleEntity
        {
            Id = model.Id,
            RoleName = model.RoleName,
            Description = model.Description,
            LastChanged = DateTimeOffset.UtcNow
        };
    }

    public static UserModel ToDomainModel(this UserEntity entity)
    {
        return new UserModel
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            Name = new NameModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Verified = entity.NameVerified
            },
            Roles = entity.Roles
                .Select(r => r.ToDomainModel())
                .ToArray()
        };
    }

    public static UserEntity ToEntity(this UserModel model)
    {
        return new UserEntity
        {
            Id = model.Id,
            UserName = model.UserName,
            FirstName = model.Name.FirstName,
            LastName = model.Name.LastName,
            NameVerified = model.Name.Verified,
            Email = model.Email,
            Roles = model.Roles
                .Select(r => r.ToEntity())
                .ToArray(),
            LastChanged = DateTimeOffset.UtcNow
        };
    }
}