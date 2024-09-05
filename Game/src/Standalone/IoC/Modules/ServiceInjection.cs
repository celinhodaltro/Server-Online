using Autofac;
using Game.Common.Contracts.Inspection;
using Game.Common.Contracts.Services;
using Game.Common.Contracts.World;
using Game.Creatures.Party;
using Game.Creatures.Services;
using Game.Items.Services;
using Game.Items.Services.ItemTransform;
using Game.Systems.SafeTrade;
using Game.Systems.SafeTrade.Operations;
using Game.Systems.Services;
using Game.World.Services;
using Server.Commands.Player.UseItem;

namespace Server.Standalone.IoC.Modules;

/// <summary>
///     Contains the registration of various game services and operations for the dependency injection container.
/// </summary>
public static class ServiceInjection
{
    /// <summary>
    ///     Registers various game services and operations with the dependency injection container.
    /// </summary>
    /// <param name="builder">The container builder instance.</param>
    /// <returns>The container builder instance with the registered services and operations.</returns>
    public static ContainerBuilder AddServices(this ContainerBuilder builder)
    {
        //Game services
        builder.RegisterType<DealTransaction>().As<IDealTransaction>().SingleInstance();
        builder.RegisterType<CoinTransaction>().As<ICoinTransaction>().SingleInstance();
        builder.RegisterType<SharedExperienceConfiguration>().As<ISharedExperienceConfiguration>().SingleInstance();
        builder.RegisterType<PartyInviteService>().As<IPartyInviteService>().SingleInstance();
        builder.RegisterType<SummonService>().As<ISummonService>().SingleInstance();
        builder.RegisterType<ToMapMovementService>().As<IToMapMovementService>().SingleInstance();
        builder.RegisterType<MapService>().As<IMapService>().SingleInstance();
        builder.RegisterType<MapTool>().As<IMapTool>().SingleInstance();
        builder.RegisterType<PlayerUseService>().As<IPlayerUseService>().SingleInstance();
        builder.RegisterType<ItemMovementService>().As<IItemMovementService>().SingleInstance();
        builder.RegisterType<ItemService>().As<IItemService>().SingleInstance();
        builder.RegisterType<StaticToDynamicTileService>().As<IStaticToDynamicTileService>().SingleInstance();
        builder.RegisterType<SafeTradeSystem>().SingleInstance();


        //Operations
        builder.RegisterType<TradeItemExchanger>().SingleInstance();

        //Items
        builder.RegisterType<DecayService>().As<IDecayService>().SingleInstance();
        builder.RegisterType<ItemTransformService>().As<IItemTransformService>().SingleInstance();
        builder.RegisterType<ItemRemoveService>().As<IItemRemoveService>().SingleInstance();

        //game builders
        builder.RegisterAssemblyTypes(Container.AssemblyCache).As<IInspectionTextBuilder>()
            .SingleInstance();

        //application services
        builder.RegisterType<HotkeyService>().SingleInstance();

        return builder;
    }
}