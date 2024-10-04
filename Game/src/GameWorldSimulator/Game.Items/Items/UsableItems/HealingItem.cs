using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Items.Cumulatives;

namespace Game.Items.Items.UsableItems;

public class HealingItem : Cumulative, IConsumable
{
    public HealingItem(IItemType type, Location location, IDictionary<ItemAttribute, IConvertible> attributes) :
        base(type, location, attributes)
    {
    }

    public ushort Min => Metadata.Attributes.GetInnerAttributes(ItemAttribute.Healing)
        ?.GetAttribute<ushort>(ItemAttribute.Min) ?? 0;

    public ushort Max => Metadata.Attributes.GetInnerAttributes(ItemAttribute.Healing)
        ?.GetAttribute<ushort>(ItemAttribute.Max) ?? 0;

    public string Type => Metadata.Attributes.GetAttribute(ItemAttribute.Healing);

    public event Use OnUsed;

    public void Use(IPlayer usedBy, ICreature creature)
    {
        if (creature is not ICombatActor actor) return;
        if (Max == 0) return;

        var value = (ushort)GameRandom.Random.Next(Min, maxValue: Max);

        if (Type.Equals("hp", StringComparison.InvariantCultureIgnoreCase))
            actor.Heal(value, usedBy);
        else if (creature is IPlayer player) player.HealMana(value);

        Reduce();

        OnUsed?.Invoke(usedBy, creature, this);
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.Healing;
    }
}