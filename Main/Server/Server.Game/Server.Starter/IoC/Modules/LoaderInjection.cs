using Autofac;
using Loader.Interfaces;
using Loader.Items;
using Loader.Monsters;
using Loader.Players;
using Loader.Quest;
using Loader.Spawns;
using Loader.Spells;
using Loader.Vocations;
using Loader.World;
using Server.Contracts.Contracts;

namespace Server.Start.IoC.Modules;

public static class LoaderInjection
{
    public static ContainerBuilder AddLoader(this ContainerBuilder builder)
    {
        builder.RegisterType<ItemTypeLoader>().SingleInstance();
        builder.RegisterType<WorldLoader>().SingleInstance();
        builder.RegisterType<SpawnLoader>().SingleInstance();
        builder.RegisterType<MonsterLoader>().SingleInstance();
        builder.RegisterType<VocationLoader>().SingleInstance();
        builder.RegisterPlayerLoader();
        builder.RegisterStartupLoader();
        builder.RegisterType<SpellLoader>().SingleInstance();
        builder.RegisterType<QuestDataLoader>().SingleInstance();

        builder.RegisterCustomLoader();

        builder.RegisterAssemblyTypes(Container.AssemblyCache).As<IRunBeforeLoader>()
            .SingleInstance();
        builder.RegisterAssemblyTypes(Container.AssemblyCache).As<IStartup>().SingleInstance();


        return builder;
    }

    private static void RegisterPlayerLoader(this ContainerBuilder builder)
    {
        var types = Container.AssemblyCache;
        builder.RegisterAssemblyTypes(types).As<PlayerLoader>().SingleInstance();
    }

    private static void RegisterStartupLoader(this ContainerBuilder builder)
    {
        var types = Container.AssemblyCache;
        builder.RegisterAssemblyTypes(types).As<IStartupLoader>().SingleInstance();
    }

    private static void RegisterCustomLoader(this ContainerBuilder builder)
    {
        builder.RegisterAssembliesByInterface(typeof(ICustomLoader));
    }
}