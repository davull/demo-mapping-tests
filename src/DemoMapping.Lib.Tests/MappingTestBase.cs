using AutoBogus;
using Bogus;

namespace DemoMapping.Lib.Tests;

public abstract class MappingTestBase : TestBase
{
    protected static T AutoFake<T>() where T : class
    {
        Faker.DefaultStrictMode = true;
        Randomizer.Seed = new Random(1234);

        var autoFaker = AutoFaker.Create(builder =>
        {
            builder
                .WithRepeatCount(2)
                .WithRecursiveDepth(3)
                .WithOverride(new BoolAutoGeneratorOverride())
                .WithOverride(new IntAutoGeneratorOverride());
        });

        return autoFaker.Generate<T>();
    }

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