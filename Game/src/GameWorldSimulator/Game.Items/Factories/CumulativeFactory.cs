using System;
using System.Collections.Generic;
using Game.Common.Contracts;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Items.Types;
using Game.Common.Item;
using Game.Common.Location.Structs;
using Game.Items.Items.Cumulatives;
using Game.Items.Items.UsableItems;

namespace Game.Items.Factories;

public class CumulativeFactory : IFactory
{
    public event CreateItem OnItemCreated;


    public IItem Create(IItemType itemType, Location location, IDictionary<ItemAttribute, IConvertible> attributes)
    {
        if (!ICumulative.IsApplicable(itemType)) return null;

        if (Coin.IsApplicable(itemType)) return new Coin(itemType, location, attributes);
        if (HealingItem.IsApplicable(itemType)) return new HealingItem(itemType, location, attributes);
        if (Food.IsApplicable(itemType)) return new Food(itemType, location, attributes);

        return new Cumulative(itemType, location, attributes);
    }
}