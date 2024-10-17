using Autofac;
using Game.Combat.Spells;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Services;
using Game.Systems.SafeTrade;
using Server.Contracts.Contracts;
using Server.Events.Combat;
using Server.Events.Creature;
using Server.Events.Player;
using Server.Events.Player.Trade;
using Server.Events.Server;
using Server.Events.Tiles;

namespace Server.Events.Subscribers;

public sealed class EventSubscriber
{
    private readonly IComponentContext _container;
    private readonly IGameServer _gameServer;

    private readonly IMap _map;
    private readonly SafeTradeSystem _tradeSystem;

    public EventSubscriber(IMap map, IGameServer gameServer, IComponentContext container, SafeTradeSystem tradeSystem)
    {
        _map = map;
        _gameServer = gameServer;
        _container = container;
        _tradeSystem = tradeSystem;
    }

    public void AttachEvents()
    {
        _map.OnCreatureAddedOnMap += (creature, cylinder) =>
            _container.Resolve<CreatureAddedOnMapEventHandler>().Execute(creature, cylinder);

        _map.OnCreatureAddedOnMap += (creature, _) =>
            _container.Resolve<PlayerSelfAppearOnMapEventHandler>().Execute(creature);

        _map.OnThingRemovedFromTile += _container.Resolve<ThingRemovedFromTileEventHandler>().Execute;
        _map.OnCreatureMoved += _container.Resolve<CreatureMovedEventHandler>().Execute;
        _map.OnThingMovedFailed += _container.Resolve<InvalidOperationEventHandler>().Execute;
        _map.OnThingAddedToTile += _container.Resolve<ThingAddedToTileEventHandler>().Execute;
        _map.OnThingUpdatedOnTile += _container.Resolve<ThingUpdatedOnTileEventHandler>().Execute;

        BaseSpell.OnSpellInvoked += _container.Resolve<SpellInvokedEventHandler>().Execute;

        OperationFailService.OnOperationFailed += _container.Resolve<PlayerOperationFailedEventHandler>().Execute;
        OperationFailService.OnInvalidOperation += _container.Resolve<PlayerOperationFailedEventHandler>().Execute;
        NotificationSenderService.OnNotificationSent += _container.Resolve<NotificationSentEventHandler>().Execute;
        _gameServer.OnOpened += _container.Resolve<ServerOpenedEventHandler>().Execute;

        AddTradeHandlers();
    }

    private void AddTradeHandlers()
    {
        _tradeSystem.OnClosed += _container.Resolve<TradeClosedEventHandler>().Execute;
        _tradeSystem.OnTradeRequest += _container.Resolve<TradeRequestedEventHandler>().Execute;
    }
}