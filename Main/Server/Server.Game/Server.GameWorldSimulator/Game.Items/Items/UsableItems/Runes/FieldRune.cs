using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Runes;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;

namespace Game.Items.Items.UsableItems.Runes;

public class FieldRune : Rune, IFieldRune
{
    public FieldRune(IItemType type, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(
        type, location, attributes)
    {
    }

    public FieldRune(IItemType type, Location location, byte amount) : base(type, location, amount)
    {
    }

    public override ushort Duration => 2;
    public event UseOnTile OnUsedOnTile;
    public ushort Field => Metadata.Attributes.GetAttribute<ushort>(ItemAttribute.Field);

    public virtual string Area => Metadata.Attributes.GetAttribute(ItemAttribute.Area);

    public bool Use(ICreature usedBy, ITile tile)
    {
        if (tile is not IDynamicTile dynamicTile) return false;
        OnUsedOnTile?.Invoke(usedBy, dynamicTile, this);

        Reduce();

        return true;
    }

    public new static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.FieldRune;
    }
}