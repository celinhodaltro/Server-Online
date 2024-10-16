using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
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