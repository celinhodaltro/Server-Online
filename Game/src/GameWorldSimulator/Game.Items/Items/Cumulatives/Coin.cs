using System;
using System.Collections.Generic;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Items.Types;
using Game.Common.Item;
using Game.Common.Location.Structs;

namespace Game.Items.Items.Cumulatives;

public class Coin : Cumulative, ICoin
{
    public Coin(IItemType type, Location location, IDictionary<ItemAttribute, IConvertible> attributes) : base(type,
        location, attributes)
    {
    }

    public Coin(IItemType type, Location location, byte amount) : base(type, location, amount)
    {
    }

    private uint WorthMultiplier => Metadata.Attributes.GetAttribute<uint>(ItemAttribute.Worth);
    public uint Worth => Amount * WorthMultiplier;

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.Coin;
    }
}