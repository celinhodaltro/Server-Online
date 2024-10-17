using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Services;
using Server.Entities.Common.Texts;
using Game.Creatures;
using Game.Items.Items.UsableItems;
using Game.World.Map;
using Game.World.Services;

namespace Extensions.Items.Tools;

public class Rope : FloorChangerUsableItem, IUsableOnItem
{
    public Rope(IItemType metadata, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(
        metadata, location)
    {
    }

    public new bool Use(ICreature usedBy, IItem onItem)
    {
        if (!CanUse(usedBy, onItem))
        {
            OperationFailService.Send(usedBy.CreatureId, TextConstants.NOT_POSSIBLE);
            return false;
        }

        if (Map.Instance[onItem.Location] is not IDynamicTile tile) return false;

        onItem = tile.Ground;

        if (onItem.Metadata.Attributes.TryGetAttribute(ItemAttribute.FloorChange, out var floorChange) &&
            floorChange == "down")
            return PullThing(usedBy, onItem, tile);

        return base.Use(usedBy, onItem);
    }

    private static bool PullThing(ICreature usedBy, IItem item, IDynamicTile tile)
    {
        var belowFloor = item.Location.AddFloors(1);

        if (Map.Instance[belowFloor] is not IDynamicTile belowTile) return false;

        if (belowTile.Players.LastOrDefault() is { } player) return PullCreature(tile, player);

        return PullItem(usedBy, belowTile);
    }

    private static bool PullItem(ICreature usedBy, IDynamicTile belowTile)
    {
        var result = belowTile.RemoveTopItem();
        if (result.Failed) return false;

        usedBy.Tile.AddItem(result.Value);
        return true;
    }

    private static bool PullCreature(IDynamicTile tile, IPlayer player)
    {
        var found = MapService
            .Instance
            .GetNeighbourAvailableTile(tile.Location, player, PlayerEnterTileRule.Rule, out var destinationTile);

        if (!found) return false;

        Map.Instance.TryMoveCreature(player, destinationTile.Location);
        return true;
    }
}