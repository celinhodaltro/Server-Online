using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Item;
using Game.Items.Items.Attributes;

namespace Game.Items.Factories.AttributeFactory;

public class ChargeableFactory : IFactory
{
    public event CreateItem OnItemCreated;

    public IChargeable Create(IItemType itemType)
    {
        if (!itemType.Attributes.TryGetAttribute<ushort>(ItemAttribute.Charges, out var charges)) return null;
        if (!itemType.Attributes.TryGetAttribute<ushort>(ItemAttribute.ShowCharges, out var showCharges))
            return new Chargeable(charges, true);

        return new Chargeable(charges, showCharges == 1);
    }
}