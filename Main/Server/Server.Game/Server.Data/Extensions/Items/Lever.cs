using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Bases;
using Game.Items.Factories;
using Game.World.Map;
using Game.World.Models.Tiles;

namespace Extensions.Items;

public class Lever : BaseItem
{
    public Lever(IItemType metadata, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(
        metadata, location)
    {
    }

    public override void Use(IPlayer usedBy)
    {
        SwitchLever();
    }

    public void SwitchLever()
    {
        if (Map.Instance[Location] is not DynamicTile dynamicTile) return;

        var newLeverId = (ushort)(Metadata.TypeId == 1946 ? 1945 : 1946);
        var newLever = ItemFactory.Instance.Create(newLeverId, Location,
            Metadata.Attributes.ToDictionary<ItemAttribute, IConvertible>());

        dynamicTile.RemoveItem(this, 1, out _);
        dynamicTile.AddItem(newLever);
    }
}