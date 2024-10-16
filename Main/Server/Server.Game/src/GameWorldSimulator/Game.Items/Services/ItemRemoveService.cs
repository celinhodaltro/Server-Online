﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;

namespace Game.Items.Services;

// This class provides functionality to remove an item from the world,
// either from a player's inventory or from the ground or a container.
public class ItemRemoveService : IItemRemoveService
{
    private readonly IMap _map;

    public ItemRemoveService(IMap map)
    {
        _map = map;
    }

    public void Remove(IItem item)
    {
        if (RemoveFromMap(item)) return;
        if (RemoveFromContainer(item)) return;

        RemoveFromInventory(item);
    }

    private static bool RemoveFromContainer(IItem item)
    {
        if (item.Parent is not IContainer container) return false;
        container.RemoveItem(item, item.Amount);
        return true;
    }

    private bool RemoveFromMap(IItem item)
    {
        if (item.Location.Type is not LocationType.Ground) return false;

        var tile = _map[item.Location];
        if (tile is not IDynamicTile fromTile) return true;

        fromTile.RemoveItem(item);

        return true;
    }

    private void RemoveFromInventory(IItem item)
    {
        if (item.Location.Type is not LocationType.Slot) return;

        if (item.Parent is not IPlayer player) return;

        player.Inventory.RemoveItem(item, item.Amount, (byte)item.Location.Slot, out _);
    }
}