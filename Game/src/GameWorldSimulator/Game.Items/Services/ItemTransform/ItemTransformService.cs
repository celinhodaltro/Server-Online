using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Services;
using Game.Common.Contracts.World;
using Game.Common.Results;
using Game.Items.Services.ItemTransform.Operations;

namespace Game.Items.Services.ItemTransform;

public class ItemTransformService : IItemTransformService
{
    private readonly IItemFactory _itemFactory;
    private readonly IItemTypeStore _itemTypeStore;
    private readonly IMap _map;
    private readonly IMapService _mapService;

    public ItemTransformService(IItemFactory itemFactory, IMap map, IMapService mapService,
        IItemTypeStore itemTypeStore)
    {
        _itemFactory = itemFactory;
        _map = map;
        _mapService = mapService;
        _itemTypeStore = itemTypeStore;
    }

    public Result<IItem> Transform(IPlayer by, IItem fromItem, ushort toItem)
    {
        var createdItem = _itemFactory.Create(toItem, fromItem.Location, null);

        _itemTypeStore.TryGetValue(toItem, out var toItemType);

        var result = ReplaceItemFromGroundOperation.Execute(_map, _itemFactory, fromItem, toItemType);
        if (!result.IsNotApplicable) return result;

        result = ReplaceItemOnInventoryOperation.Execute(_itemFactory, fromItem, toItemType);
        if (!result.IsNotApplicable) return result;

        result = ReplaceGroundOperation.Execute(_map, _mapService, fromItem, createdItem);
        if (!result.IsNotApplicable) return result;

        result = ReplaceItemOnContainerOperation.Execute(by, fromItem, createdItem);
        if (!result.IsNotApplicable) return result;

        return Result<IItem>.Ok(null);
    }

    public Result<IItem> Transform(IItem fromItem, ushort toItem)
    {
        return Transform(null, fromItem, toItem);
    }
}