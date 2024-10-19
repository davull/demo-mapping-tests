﻿using FluentAssertions;
using Snapshooter.Xunit;
using Xunit;

namespace DemoMapping.Lib.Tests;

public class MappingTests : MappingTestBase
{
    private const int Count = 10;

    [Fact]
    public void Role_Entity_ToDomainModel_ShouldMatchSnapshot()
    {
        var entity = AutoFake<RoleEntity>();
        var model = entity.ToDomainModel();

        var snapshot = new { entity, model };
        snapshot.Should().MatchSnapshot(opt => opt
            .IgnoreField("**.LastChanged"));
    }

    [Theory]
    [MemberData(nameof(RoleEntitySource))]
    public void Role_Entity_DomainModel_Roundtrip(RoleEntity source)
    {
        var target = source.ToDomainModel().ToEntity();

        source.Should().BeEquivalentTo(target, opt => opt
            .Excluding(e => e.LastChanged)
            .Excluding(e => e.IsDeleted));
    }

    public static IEnumerable<object[]> RoleEntitySource()
        => AutoFakes<RoleEntity>(Count).Select(e => new object[] { e });

    [Theory]
    [MemberData(nameof(RoleModelSource))]
    public void Role_DomainModel_Entity_Roundtrip(RoleModel source)
    {
        var target = source.ToEntity().ToDomainModel();
        source.Should().BeEquivalentTo(target);
    }

    public static IEnumerable<object[]> RoleModelSource()
        => AutoFakes<RoleModel>(Count.Select(e => new object[] { e });

    [Fact]
    public void User_Entity_ToDomainModel_ShouldMatchSnapshot()
    {
        var entity = AutoFake<UserEntity>();
        var model = entity.ToDomainModel();

        var snapshot = new { entity, model };
        snapshot.Should().MatchSnapshot(opt => opt
            .IgnoreField("**.LastChanged"));
    }

    [Theory]
    [MemberData(nameof(UserEntitySource))]
    public void User_Entity_DomainModel_Roundtrip(UserEntity source)
    {
        var target = source.ToDomainModel().ToEntity();

        source.Should().BeEquivalentTo(target, opt => opt
            .Excluding(e => e.LastChanged)
            .Excluding(e => e.IsDeleted)
            .For(e => e.Roles).Exclude(r => r.LastChanged)
            .For(e => e.Roles).Exclude(r => r.IsDeleted));
    }

    public static IEnumerable<object[]> UserEntitySource()
        => AutoFakes<UserEntity>(Count).Select(e => new object[] { e });

    [Theory]
    [MemberData(nameof(UserModelSource))]
    public void User_DomainModel_Entity_Roundtrip(UserModel source)
    {
        var target = source.ToEntity().ToDomainModel();

        source.Should().BeEquivalentTo(target);
    }

    public static IEnumerable<object[]> UserModelSource()
        => AutoFakes<UserModel>(Count).Select(e => new object[] { e });
}