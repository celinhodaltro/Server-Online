using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Items;

namespace Game.Items.Factories;

public class LiquidPoolFactory : ILiquidPoolFactory
{
    private readonly IItemTypeStore _itemTypeStore;

    public LiquidPoolFactory(IItemTypeStore itemTypeStore)
    {
        _itemTypeStore = itemTypeStore;
    }

    public event CreateItem OnItemCreated;

    public ILiquid Create(Location location, LiquidColor color)
    {
        if (!_itemTypeStore.TryGetValue(2016, out var itemType)) return null;

        if (itemType.Group == ItemGroup.Deprecated) return null;

        var item = new LiquidPool(itemType, location, color);
        OnItemCreated?.Invoke(item);
        return item;
    }

    public ILiquid CreateDamageLiquidPool(Location location, LiquidColor color)
    {
        if (!_itemTypeStore.TryGetValue(2019, out var itemType)) return null;

        if (itemType.Group == ItemGroup.Deprecated) return null;

        var item = new LiquidPool(itemType, location, color);
        OnItemCreated?.Invoke(item);
        return item;
    }
}