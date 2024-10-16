﻿using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Items.Cumulatives;

namespace Game.Items.Items.UsableItems;

public class Food : Cumulative, IConsumable, IFood
{
    public Food(IItemType type, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(type,
        location, attributes)
    {
    }

    public Food(IItemType type, Location location, byte amount) : base(type,
        location, amount)
    {
    }

    public event Use OnUsed;
    public int CooldownTime => 0;

    public void Use(IPlayer usedBy, ICreature creature)
    {
        if (creature is not IPlayer player) return;

        if (!player.Feed(this)) return;

        Reduce();

        OnUsed?.Invoke(usedBy, creature, this);
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.Food;
    }
}