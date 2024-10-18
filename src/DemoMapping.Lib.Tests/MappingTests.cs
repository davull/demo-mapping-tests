using AutoBogus;
using Bogus;
using FluentAssertions;
using Snapshooter.Xunit;
using Xunit;

namespace DemoMapping.Lib.Tests;

public class MappingTests : TestBase
{
    [Fact]
    public void Role_Entity_ToDomainModel_ShouldMatchSnapshot()
    {
        var entity = AutoFake<RoleEntity>();
        var model = entity.ToDomainModel();

        var snapshot = new { entity, model };
        snapshot.Should().MatchSnapshot(opt => opt
            .IgnoreField("**.LastChanged"));
    }

    [Fact]
    public void Role_Entity_DomainModel_Roundtrip()
    {
        var source = AutoFake<RoleEntity>();
        var target = source.ToDomainModel().ToEntity();

        source.Should().BeEquivalentTo(target, opt => opt
            .Excluding(e => e.LastChanged)
            .Excluding(e => e.IsDeleted));
    }

    [Fact]
    public void Role_DomainModel_Entity_Roundtrip()
    {
        var source = AutoFake<RoleModel>();
        var target = source.ToEntity().ToDomainModel();

        source.Should().BeEquivalentTo(target);
    }

    [Fact]
    public void User_Entity_ToDomainModel_ShouldMatchSnapshot()
    {
        var entity = AutoFake<UserEntity>();
        var model = entity.ToDomainModel();

        var snapshot = new { entity, model };
        snapshot.Should().MatchSnapshot(opt => opt
            .IgnoreField("**.LastChanged"));
    }

    [Fact]
    public void User_Entity_DomainModel_Roundtrip()
    {
        var source = AutoFake<UserEntity>();
        var target = source.ToDomainModel().ToEntity();

        source.Should().BeEquivalentTo(target, opt => opt
            .Excluding(e => e.LastChanged)
            .Excluding(e => e.IsDeleted)
            .For(e => e.Roles).Exclude(r => r.LastChanged)
            .For(e => e.Roles).Exclude(r => r.IsDeleted));
    }

    [Fact]
    public void User_DomainModel_Entity_Roundtrip()
    {
        var source = AutoFake<UserModel>();
        var target = source.ToEntity().ToDomainModel();

        source.Should().BeEquivalentTo(target);
    }

    private static T AutoFake<T>() where T : class
    {
        Faker.DefaultStrictMode = true;
        Randomizer.Seed = new Random(1234);

        var autoFaker = AutoFaker.Create(builder =>
        {
            builder
                .WithRepeatCount(2)
                .WithOverride(new BoolAutoGeneratorOverride())
                .WithOverride(new IntAutoGeneratorOverride());
        });

        return autoFaker.Generate<T>();
    }

    public class BoolAutoGeneratorOverride : AutoGeneratorOverride
    {
        public override bool CanOverride(AutoGenerateContext context)
            => context.GenerateType == typeof(bool);

        public override void Generate(AutoGenerateOverrideContext context)
        {
            context.Instance = true;
        }
    }

    public class IntAutoGeneratorOverride : AutoGeneratorOverride
    {
        public override bool CanOverride(AutoGenerateContext context)
            => context.GenerateType == typeof(int);

        public override void Generate(AutoGenerateOverrideContext context)
        {
            context.Instance = context.Faker.Random.Number(1, 100);
        }
    }
}