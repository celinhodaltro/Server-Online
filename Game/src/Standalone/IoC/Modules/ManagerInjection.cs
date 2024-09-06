using Autofac;
using Game.Common.Contracts.Creatures;
using Game.Creatures.Monster.Managers;
using Game.Systems.Depot;
using Game.World.Models.Spawns;
using Server.Common.Contracts;
using Server.Managers;

namespace Server.Start.IoC.Modules;

public static class ManagerInjection
{
    public static ContainerBuilder AddManagers(this ContainerBuilder builder)
    {
        builder.RegisterType<GameServer>().As<IGameServer>().SingleInstance();
        builder.RegisterType<GameCreatureManager>().As<IGameCreatureManager>().SingleInstance();
        builder.RegisterType<DecayableItemManager>().As<IDecayableItemManager>().SingleInstance();


        builder.RegisterType<MonsterDataManager>().As<IMonsterDataManager>().SingleInstance();
        builder.RegisterType<SpawnManager>().SingleInstance();
        builder.RegisterType<DepotManager>().SingleInstance();
        return builder;
    }
}