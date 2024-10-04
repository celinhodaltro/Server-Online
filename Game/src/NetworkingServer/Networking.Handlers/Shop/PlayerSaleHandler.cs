using Data.InMemory;
using Server.Entities.Common.Contracts;
using Networking.Packets.Incoming.Shop;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Shop;

public class PlayerSaleHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly IItemTypeStore _itemTypeStore;

    public PlayerSaleHandler(IGameServer game, IItemTypeStore itemTypeStore)
    {
        _game = game;
        _itemTypeStore = itemTypeStore;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var playerSalePacket = new PlayerSalePacket(message);
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        var serverId = ItemClientServerIdMapStore.Data.Get(playerSalePacket.ItemClientId);

        if (!_itemTypeStore.TryGetValue(serverId, out var itemType)) return;

        _game.Dispatcher.AddEvent(new Event(() =>
            player.Sell(itemType, playerSalePacket.Amount, playerSalePacket.IgnoreEquipped)));
    }
}