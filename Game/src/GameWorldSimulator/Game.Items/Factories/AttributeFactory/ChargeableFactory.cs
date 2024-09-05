using Game.Common.Contracts;
using Game.Common.Contracts.Items;
using Game.Common.Item;
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