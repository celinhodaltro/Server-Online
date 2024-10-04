using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Location.Structs;
using Game.Items.Factories.AttributeFactory;
using Game.Items.Items;

namespace Game.Items.Factories;

public class DefenseEquipmentFactory : IFactory
{
    private readonly ChargeableFactory _chargeableFactory;
    private readonly IItemTypeStore _itemTypeStore;

    public DefenseEquipmentFactory(IItemTypeStore itemTypeStore, ChargeableFactory chargeableFactory)
    {
        _itemTypeStore = itemTypeStore;
        _chargeableFactory = chargeableFactory;
    }

    public event CreateItem OnItemCreated;

    public BodyDefenseEquipment Create(IItemType itemType, Location location)
    {
        var chargeable = _chargeableFactory.Create(itemType);

        if (!BodyDefenseEquipment.IsApplicable(itemType)) return null;

        return new BodyDefenseEquipment(itemType, location)
        {
            Chargeable = chargeable,
            ItemTypeFinder = _itemTypeStore.Get
        };
    }
}