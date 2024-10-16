﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Server.Entities.Common.Services;
using Networking.Packets.Incoming;

namespace Server.Commands.Movements.ToInventory;

public sealed class MapToInventoryMovementOperation
{
    private readonly IItemMovementService _itemMovementService;

    public MapToInventoryMovementOperation(IItemMovementService itemMovementService)
    {
        _itemMovementService = itemMovementService;
    }

    public void Execute(IPlayer player, IMap map, ItemThrowPacket itemThrow)
    {
        FromMapToInventory(player, map, itemThrow);
    }

    private void FromMapToInventory(IPlayer player, IMap map, ItemThrowPacket itemThrow)
    {
        if (map[itemThrow.FromLocation] is not { } fromTile) return;
        if (fromTile.TopItemOnStack is not { } item) return;
        if (fromTile is not IDynamicTile dynamicTile) return;

        var result = _itemMovementService.Move(player, item, dynamicTile, player.Inventory, itemThrow.Count, 0,
            (byte)itemThrow.ToLocation.Slot);

        if (result.Failed) OperationFailService.Send(player, result.Error);
    }

    public static bool IsApplicable(ItemThrowPacket itemThrowPacket)
    {
        return itemThrowPacket.FromLocation.Type == LocationType.Ground
               && itemThrowPacket.ToLocation.Type == LocationType.Slot;
    }
}