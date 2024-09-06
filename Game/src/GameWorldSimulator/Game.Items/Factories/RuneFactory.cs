using System;
using System.Collections.Generic;
using Game.Common.Contracts;
using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Items.Types;
using Game.Common.Item;
using Game.Common.Location.Structs;
using Game.Items.Items.UsableItems.Runes;

namespace Game.Items.Factories;

public class RuneFactory : IFactory
{
    private readonly IAreaEffectStore _areaEffectStore;

    public RuneFactory(IAreaEffectStore areaEffectStore)
    {
        _areaEffectStore = areaEffectStore;
    }

    public event CreateItem OnItemCreated;

    public IItem Create(IItemType itemType, Location location,
        IDictionary<ItemAttribute, IConvertible> attributes)
    {
        if (!ICumulative.IsApplicable(itemType)) return null;
        if (!Rune.IsApplicable(itemType)) return null;

        if (AttackRune.IsApplicable(itemType))
            return new AttackRune(itemType, location, attributes) { GetAreaTypeFunc = _areaEffectStore.Get };
        if (FieldRune.IsApplicable(itemType)) return new FieldRune(itemType, location, attributes);

        return null;
    }
}