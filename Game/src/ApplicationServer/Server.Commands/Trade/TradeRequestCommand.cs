using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Creatures.Players;
using Game.Common.Helpers;
using Game.Common.Location;
using Game.Systems.SafeTrade;
using Networking.Packets.Incoming.Trade;
using Server.Common.Contracts;
using Server.Common.Contracts.Commands;

namespace Server.Commands.Trade;

public class TradeRequestCommand : ICommand
{
    private readonly IGameServer _gameServer;
    private readonly SafeTradeSystem _tradeSystem;

    public TradeRequestCommand(IGameServer gameServer, SafeTradeSystem tradeSystem)
    {
        _gameServer = gameServer;
        _tradeSystem = tradeSystem;
    }

    public void RequestTrade(IPlayer player, TradeRequestPacket packet)
    {
        if (Guard.AnyNull(player, packet)) return;

        var item = GetItem(player, packet);
        if (item is null) return;

        _gameServer.CreatureManager.TryGetPlayer(packet.PlayerId, out var secondPlayer);
        if (secondPlayer is null) return;

        _tradeSystem.Request(player, secondPlayer, item);
    }

    private IItem GetItem(IPlayer player, TradeRequestPacket packet)
    {
        if (packet.Location.Type == LocationType.Ground)
        {
            if (_gameServer.Map[packet.Location] is not { } tile) return null;
            return tile.TopItemOnStack;
        }

        if (packet.Location.Slot == Slot.Backpack)
        {
            var item = player.Inventory[Slot.Backpack];
            item?.SetNewLocation(packet.Location);
            return item;
        }

        if (packet.Location.Type == LocationType.Container)
        {
            var item = player.Containers[packet.Location.ContainerId][packet.Location.ContainerSlot];
            item?.SetNewLocation(packet.Location);
            return item;
        }

        if (packet.Location.Type == LocationType.Slot)
        {
            var item = player.Inventory[packet.Location.Slot];
            item?.SetNewLocation(packet.Location);
            return item;
        }

        return null;
    }
}