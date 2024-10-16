﻿using System;
using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;

namespace Game.Items.Items.UsableItems;

public class FloorChangerUsableItem : UsableOnItem, IUsableOnItem
{
    public FloorChangerUsableItem(IItemType type, Location location) : base(type, location)
    {
    }

    public override bool AllowUseOnDistance => false;

    public virtual bool Use(ICreature usedBy, IItem onItem)
    {
        if (usedBy is not IPlayer player) return false;
        var canUseOnItems = Metadata.OnUse?.GetAttributeArray<ushort>(ItemAttribute.UseOn) ?? Array.Empty<ushort>();

        if (!canUseOnItems.Contains(onItem.Metadata.TypeId)) return false;

        if (Metadata.OnUse?.GetAttribute(ItemAttribute.FloorChange) != "up") return false;

        var toLocation = new Location(onItem.Location.X, onItem.Location.Y, (byte)(onItem.Location.Z - 1));

        player.TeleportTo(toLocation);
        return true;
    }

    public new static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.UsableFloorChanger;
    }
}