using Data.InMemory;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Services;
using Networking.Packets.Incoming.Shop;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Shop;

public class PlayerPurchaseHandler : PacketHandler
{
    private readonly IDealTransaction _dealTransaction;
    private readonly IGameServer _game;
    private readonly IItemTypeStore _itemTypeStore;

    public PlayerPurchaseHandler(IGameServer game, IDealTransaction dealTransaction, IItemTypeStore itemTypeStore)
    {
        _game = game;
        _dealTransaction = dealTransaction;
        _itemTypeStore = itemTypeStore;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var playerPurchasePacket = new PlayerPurchasePacket(message);
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        var serverId = ItemClientServerIdMapStore.Data.Get(playerPurchasePacket.ItemClientId);

        if (!_itemTypeStore.TryGetValue(serverId, out var itemType)) return;

        _game.Dispatcher.AddEvent(new Event(() =>
            _dealTransaction?.Buy(player, player.TradingWithNpc, itemType, playerPurchasePacket.Amount)));
    }
}