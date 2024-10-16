﻿using System;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location;
using Server.Entities.Common.Location.Structs;

namespace Game.World.Algorithms;

public static class TileDestinationCalculation
{
    public static ITile Calculate(ITile tile, IMap map)
    {
        return GetTileDestination(tile, map);
    }

    private static ITile GetTileDestination(ITile tile, IMap map)
    {
        if (tile is not IDynamicTile toTile) return tile;

        var topItemOnStack = tile.TopItemOnStack;

        if (IsTeleportTile(topItemOnStack)) return GetTeleportDestinationTile(topItemOnStack, map) ?? tile;

        if (HasFloorDestination(tile, FloorChangeDirection.Down)) return GetDownTileDestination(tile, map) ?? tile;

        if (toTile.FloorDirection != default) //has any floor destination check
            return GetUpTileDestination(tile, map) ?? tile;

        return tile;
    }

    private static bool HasFloorDestination(ITile tile, FloorChangeDirection direction)
    {
        return tile is IDynamicTile walkable && walkable.FloorDirection == direction;
    }

    private static bool IsTeleportTile(IItem topItemOnStack)
    {
        return topItemOnStack.Metadata.Attributes.GetAttribute(ItemAttribute.Type)
            .Equals("teleport", StringComparison.InvariantCultureIgnoreCase);
    }

    private static ITile GetTeleportDestinationTile(IItem topItemOnStack, IMap map)
    {
        return topItemOnStack.Metadata.Attributes.TryGetAttribute<Location>(
            ItemAttribute.TeleportDestination, out var teleportDestination)
            ? map[teleportDestination]
            : null;
    }

    private static ITile GetDownTileDestination(ITile tile, IMap map)
    {
        var x = tile.Location.X;
        var y = tile.Location.Y;
        var z = tile.Location.Z;
        z++;

        var southDownTile = map[x, (ushort)(y - 1), z];

        if (HasFloorDestination(southDownTile, FloorChangeDirection.SouthAlternative))
        {
            y -= 2;
            return map[x, y, z];
        }

        var eastDownTile = map[(ushort)(x - 1), y, z];

        if (HasFloorDestination(eastDownTile, FloorChangeDirection.EastAlternative))
        {
            x -= 2;
            return map[x, y, z];
        }

        var downTile = map[x, y, z];

        if (downTile == null) return null;

        if (HasFloorDestination(downTile, FloorChangeDirection.North)) ++y;
        if (HasFloorDestination(downTile, FloorChangeDirection.South)) --y;
        if (HasFloorDestination(downTile, FloorChangeDirection.SouthAlternative)) y -= 2;
        if (HasFloorDestination(downTile, FloorChangeDirection.East)) --x;
        if (HasFloorDestination(downTile, FloorChangeDirection.EastAlternative)) x -= 2;
        if (HasFloorDestination(downTile, FloorChangeDirection.West)) ++x;

        return map[x, y, z];
    }

    private static ITile GetUpTileDestination(ITile tile, IMap map)
    {
        var x = tile.Location.X;
        var y = tile.Location.Y;
        var z = tile.Location.Z;

        z--;

        if (HasFloorDestination(tile, FloorChangeDirection.North)) --y;
        if (HasFloorDestination(tile, FloorChangeDirection.South)) ++y;
        if (HasFloorDestination(tile, FloorChangeDirection.SouthAlternative)) y += 2;
        if (HasFloorDestination(tile, FloorChangeDirection.East)) ++x;
        if (HasFloorDestination(tile, FloorChangeDirection.EastAlternative)) x += 2;
        if (HasFloorDestination(tile, FloorChangeDirection.West)) --x;

        return map[x, y, z] ?? tile;
    }
}