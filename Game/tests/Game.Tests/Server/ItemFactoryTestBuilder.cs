using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.World;
using Game.Items.Factories;
using Game.Items.Factories.AttributeFactory;

namespace Game.Tests.Server;

public static class ItemFactoryTestBuilder
{
    public static IItemFactory Build(params IItemType[] itemTypes)
    {
        var itemTypeStore = ItemTypeStoreTestBuilder.Build(itemTypes);

        return new ItemFactory
        {
            WeaponFactory = new WeaponFactory(new ChargeableFactory(), itemTypeStore),
            ItemTypeStore = itemTypeStore
        };
    }

    public static IItemFactory Build(IItemTypeStore itemTypeStore, IMap map = null)
    {
        var chargeableFactory = new ChargeableFactory();

        var itemFactory = new ItemFactory
        {
            WeaponFactory = new WeaponFactory(chargeableFactory, itemTypeStore),
            DefenseEquipmentFactory = new DefenseEquipmentFactory(itemTypeStore, chargeableFactory),
            ItemTypeStore = itemTypeStore
        };

        return itemFactory;
    }
}