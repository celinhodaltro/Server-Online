﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Location;
using Networking.Packets.Incoming;

namespace Server.Commands.Movements.ToContainer;

public class InventoryToContainerMovementOperation
{
    public static void Execute(IPlayer player, ItemThrowPacket itemThrow)
    {
        var container = player.Containers[itemThrow.ToLocation.ContainerId];

        if (container is null) return;

        var item = player.Inventory[itemThrow.FromLocation.Slot];

        if (item is null) return;
        if (!item.IsPickupable) return;

        player.MoveItem(item, player.Inventory, container, itemThrow.Count, (byte)itemThrow.FromLocation.Slot,
            (byte)itemThrow.ToLocation.ContainerSlot);
    }

    public static bool IsApplicable(ItemThrowPacket itemThrowPacket)
    {
        return itemThrowPacket.FromLocation.Type == LocationType.Slot
               && itemThrowPacket.ToLocation.Type == LocationType.Container;
    }
}