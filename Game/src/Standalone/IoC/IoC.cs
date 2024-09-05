using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.World;
using Game.Creatures;
using Game.World;
using Game.World.Map;
using Networking.Handlers;
using Server.Commands.Movements;
using Server.Commands.Player;
using Server.Common.Contracts.Tasks;
using Server.Standalone.IoC.Modules;
using Server.Tasks;
using Shared.IoC.Modules;
using PathFinder = Game.World.Map.PathFinder;

namespace Server.Standalone.IoC;

public static class Container
{
    internal static Assembly[] AssemblyCache => AppDomain.CurrentDomain.GetAssemblies().AsParallel().Where(assembly =>
        !assembly.IsDynamic &&
        !assembly.FullName.StartsWith("System.") &&
        !assembly.FullName.StartsWith("Microsoft.") &&
        !assembly.FullName.StartsWith("Windows.") &&
        !assembly.FullName.StartsWith("mscorlib,") &&
        !assembly.FullName.StartsWith("Serilog,") &&
        !assembly.FullName.StartsWith("Autofac,") &&
        !assembly.FullName.StartsWith("netstandard,")).ToArray();

    public static IContainer BuildConfigurations()
    {
        var builder = new ContainerBuilder();

        var configuration = ConfigurationInjection.GetConfiguration();

        builder
            .AddConfigurations(configuration)
            .AddLogger(configuration);

        return builder.Build();
    }

    public static IContainer BuildAll()
    {
        var builder = new ContainerBuilder();

        //tools
        builder.RegisterType<PathFinder>().As<IPathFinder>().SingleInstance();
        builder.RegisterType<WalkToMechanism>().As<IWalkToMechanism>().SingleInstance();

        builder.RegisterPacketHandlers();

        builder.RegisterType<OptimizedScheduler>().As<IScheduler>().SingleInstance();
        builder.RegisterType<Dispatcher>().As<IDispatcher>().SingleInstance();
        builder.RegisterType<PersistenceDispatcher>().As<IPersistenceDispatcher>().SingleInstance();

        //world
        builder.RegisterType<Map>().As<IMap>().SingleInstance();
        builder.RegisterType<World>().SingleInstance();

        var configuration = ConfigurationInjection.GetConfiguration();

        builder.AddFactories()
            .AddServices()
            .AddLoader()
            .AddDatabases(configuration)
            .AddRepositories()
            .AddConfigurations(configuration)
            .AddNetwork()
            .AddEvents()
            .AddManagers()
            .AddLogger(configuration)
            .AddCommands()
            .AddLua()
            .AddJobs()
            .AddCommands()
            .AddDataStores();

        //creature
        builder.RegisterType<CreatureGameInstance>().As<ICreatureGameInstance>().SingleInstance();

        builder.RegisterInstance(new MemoryCache(new MemoryCacheOptions())).As<IMemoryCache>();

        return builder.Build();
    }

    private static void RegisterPacketHandlers(this ContainerBuilder builder)
    {
        var assemblies = Assembly.GetAssembly(typeof(PacketHandler));
        builder.RegisterAssemblyTypes(assemblies).SingleInstance();
    }

    private static ContainerBuilder AddCommands(this ContainerBuilder builder)
    {
        var assembly = Assembly.GetAssembly(typeof(PlayerLogInCommand));
        builder.RegisterAssemblyTypes(assembly);
        return builder;
    }
}