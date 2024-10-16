using System.Collections.Generic;
using Autofac;
using Game.Chats;
using Server.Entities.Common.Contracts.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World.Tiles;
using Game.Creatures.Factories;
using Game.Creatures.Player;
using Game.Items.Factories;
using Game.Items.Factories.AttributeFactory;
using Game.World.Factories;
using Networking.Handlers;
using Networking.Handlers.Invalid;
using Server.Common.Contracts.Network;
using Server.Common.Contracts.Network.Enums;
using Serilog;

namespace Server.Start.IoC.Modules;

public static class FactoryInjection
{
    public static ContainerBuilder AddFactories(this ContainerBuilder builder)
    {
        builder.RegisterType<ItemFactory>().As<IItemFactory>().OnActivated(e =>
            {
                e.Instance.DefenseEquipmentFactory = e.Context.Resolve<DefenseEquipmentFactory>();
                e.Instance.WeaponFactory = e.Context.Resolve<WeaponFactory>();
                e.Instance.ContainerFactory = e.Context.Resolve<ContainerFactory>();
                e.Instance.GroundFactory = e.Context.Resolve<GroundFactory>();
                e.Instance.RuneFactory = e.Context.Resolve<RuneFactory>();
                e.Instance.CumulativeFactory = e.Context.Resolve<CumulativeFactory>();
                e.Instance.GenericItemFactory = e.Context.Resolve<GenericItemFactory>();
                e.Instance.ItemEventSubscribers = e.Context.Resolve<IEnumerable<IItemEventSubscriber>>();
                e.Instance.ItemTypeStore = e.Context.Resolve<IItemTypeStore>();
                e.Instance.CoinTypeStore = e.Context.Resolve<ICoinTypeStore>();
            })
            .SingleInstance();

        builder.RegisterType<DefenseEquipmentFactory>().SingleInstance();
        builder.RegisterType<WeaponFactory>().SingleInstance();
        builder.RegisterType<ContainerFactory>().SingleInstance();
        builder.RegisterType<GroundFactory>().SingleInstance();
        builder.RegisterType<RuneFactory>().SingleInstance();
        builder.RegisterType<CumulativeFactory>().SingleInstance();
        builder.RegisterType<GenericItemFactory>().SingleInstance();

        builder.RegisterType<ProtectionFactory>().SingleInstance();
        builder.RegisterType<DecayableFactory>().SingleInstance();
        builder.RegisterType<SkillBonusFactory>().SingleInstance();
        builder.RegisterType<ChargeableFactory>().SingleInstance();

        builder.RegisterType<ChatChannelFactory>().OnActivated(e =>
            {
                e.Instance.ChannelEventSubscribers =
                    e.Context.Resolve<IEnumerable<IChatChannelEventSubscriber>>();
                e.Instance.ChatChannelStore =
                    e.Context.Resolve<IChatChannelStore>();
                e.Instance.GuildStore =
                    e.Context.Resolve<IGuildStore>();
            })
            .SingleInstance();

        builder.RegisterType<LiquidPoolFactory>().As<ILiquidPoolFactory>().SingleInstance();

        builder.RegisterType<CreatureFactory>().As<ICreatureFactory>().SingleInstance();
        builder.RegisterType<MonsterFactory>().As<IMonsterFactory>().SingleInstance();
        builder.RegisterType<NpcFactory>().As<INpcFactory>().SingleInstance();
        builder.RegisterType<TileFactory>().As<ITileFactory>().SingleInstance();

        builder.RegisterPlayerFactory();
        builder.RegisterIncomingPacketFactory();

        return builder;
    }

    private static void RegisterIncomingPacketFactory(this ContainerBuilder builder)
    {
        bool RequireAuthentication(GameIncomingPacketType gameIncomingPacketType)
        {
            return gameIncomingPacketType is not (GameIncomingPacketType.PlayerLogIn
                or GameIncomingPacketType.PlayerLoginRequest);
        }

        builder.Register((c, p) =>
        {
            var conn = p.TypedAs<IConnection>();

            var packet = GameIncomingPacketType.PlayerLogOut;

            if (!conn.Disconnected) packet = conn.InMessage.GetIncomingPacketType(conn.IsAuthenticated);

            if (!conn.IsAuthenticated && RequireAuthentication(packet))
            {
                return new NotAllowedPacketHandler(packet, c.Resolve<ILogger>());
            }
            
            if (!InputHandlerMap.Data.TryGetValue(packet, out var handlerType))
                return new NotImplementedPacketHandler(packet, c.Resolve<ILogger>());

            if (!c.TryResolve(handlerType, out var instance))
                return new NotImplementedPacketHandler(packet, c.Resolve<ILogger>());

            c.Resolve<ILogger>().Debug("{Incoming}: {Packet}", "Incoming Packet", packet);

            return (IPacketHandler)instance;
        });
    }

    private static void RegisterPlayerFactory(this ContainerBuilder builder)
    {
        builder.Register((c, p) =>
        {
            var player = p.TypedAs<Player>();

            return c.Resolve<ICreatureFactory>().CreatePlayer(player);
        });
    }
}