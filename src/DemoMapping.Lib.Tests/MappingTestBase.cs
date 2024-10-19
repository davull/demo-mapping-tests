using AutoBogus;
using Bogus;

namespace DemoMapping.Lib.Tests;

public abstract class MappingTestBase : TestBase
{
    protected MappingTestBase()
    {
        Faker.DefaultStrictMode = true;
        Randomizer.Seed = new Random(1234);
    }

    public static IEnumerable<object[]> AutoFakeTestSource<T>(int count)
        where T : class
    {
        return AutoFakes<T>(count).Select(e => new object[] { e });
    }

    protected static List<T> AutoFakes<T>(int count) where T : class
    {
        var autoFaker = AutoFaker.Create(builder =>
        {
            builder
                .WithRepeatCount(2)
                .WithRecursiveDepth(3)
                .WithOverride(new BoolAutoGeneratorOverride())
                .WithOverride(new IntAutoGeneratorOverride());
        });

        return autoFaker.Generate<T>(count);
    }

    protected static T AutoFake<T>() where T : class
        => AutoFakes<T>(1).Single();

    private class BoolAutoGeneratorOverride : AutoGeneratorOverride
    {
        public override bool CanOverride(AutoGenerateContext context)
            => context.GenerateType == typeof(bool);

        public override void Generate(AutoGenerateOverrideContext context)
        {
            context.Instance = true;
        }
    }

    private class IntAutoGeneratorOverride : AutoGeneratorOverride
    {
        public override bool CanOverride(AutoGenerateContext context)
            => context.GenerateType == typeof(int);

        public override void Generate(AutoGenerateOverrideContext context)
        {
            context.Instance = context.Faker.Random.Number(1, 100);
        }
    }
}