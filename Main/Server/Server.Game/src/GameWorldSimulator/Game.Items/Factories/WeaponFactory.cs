using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Factories.AttributeFactory;
using Game.Items.Items.Weapons;

namespace Game.Items.Factories;

public class WeaponFactory : IFactory
{
    private readonly ChargeableFactory _chargeableFactory;
    private readonly IItemTypeStore _itemTypeStore;

    public WeaponFactory(ChargeableFactory chargeableFactory, IItemTypeStore itemTypeStore)
    {
        _chargeableFactory = chargeableFactory;
        _itemTypeStore = itemTypeStore;
    }

    public event CreateItem OnItemCreated;

    public IItem Create(IItemType itemType, Location location, IDictionary<ItemAttribute, IConvertible> attributes)
    {
        var chargeable = _chargeableFactory.Create(itemType);

        if (MeleeWeapon.IsApplicable(itemType))
            return new MeleeWeapon(itemType, location)
            {
                Chargeable = chargeable,
                ItemTypeFinder = _itemTypeStore.Get
            };
        if (DistanceWeapon.IsApplicable(itemType))
            return new DistanceWeapon(itemType, location)
            {
                ItemTypeFinder = _itemTypeStore.Get,
                Chargeable = chargeable
            };
        if (MagicWeapon.IsApplicable(itemType))
            return new MagicWeapon(itemType, location)
            {
                ItemTypeFinder = _itemTypeStore.Get,
                Chargeable = chargeable
            };

        if (ICumulative.IsApplicable(itemType))
        {
            if (ThrowableDistanceWeapon.IsApplicable(itemType))
                return new ThrowableDistanceWeapon(itemType, location, attributes);
            if (Ammo.IsApplicable(itemType)) return new Ammo(itemType, location, attributes);
        }

        return null;
    }
}