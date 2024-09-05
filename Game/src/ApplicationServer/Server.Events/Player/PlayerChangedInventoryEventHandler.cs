using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;
using Game.Common.Creatures.Players;
using Game.Common.Helpers;
using Networking.Packets.Outgoing.Npc;
using Networking.Packets.Outgoing.Player;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerChangedInventoryEventHandler
{
    private readonly ICoinTypeStore _coinTypeStore;
    private readonly IGameServer game;

    public PlayerChangedInventoryEventHandler(IGameServer game, ICoinTypeStore coinTypeStore)
    {
        this.game = game;
        _coinTypeStore = coinTypeStore;
    }

    public void Execute(IInventory inventory, IItem item, Slot slot, byte amount = 1)
    {
        if (Guard.AnyNull(inventory)) return;

        var player = inventory.Owner;

        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new PlayerInventoryItemPacket(player.Inventory, slot));

        if (player.Shopping)
            connection.OutgoingPackets.Enqueue(new SaleItemListPacket(player,
                player.TradingWithNpc?.ShopItems?.Values, _coinTypeStore));

        connection.Send();
    }

    public void ExecuteOnWeightChanged(IInventory inventory)
    {
        if (Guard.AnyNull(inventory)) return;

        var player = inventory.Owner;

        if (!game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new PlayerStatusPacket(player));

        connection.Send();
    }
}