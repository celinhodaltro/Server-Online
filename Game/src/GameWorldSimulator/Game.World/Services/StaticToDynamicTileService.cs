using System;
using System.Collections.Generic;
using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Services;
using Game.Common.Contracts.World;
using Game.Common.Contracts.World.Tiles;
using Game.Common.Item;
using Game.Common.Location;
using Game.Common.Location.Structs;

namespace Game.World.Services;

public class StaticToDynamicTileService : IStaticToDynamicTileService
{
    private readonly IItemClientServerIdMapStore _itemClientServerIdMapStore;
    private readonly IItemFactory _itemFactory;
    private readonly IMap _map;
    private readonly ITileFactory _tileFactory;

    public StaticToDynamicTileService(IItemClientServerIdMapStore itemClientServerIdMapStore, IItemFactory itemFactory,
        ITileFactory tileFactory, IMap map)
    {
        _itemClientServerIdMapStore = itemClientServerIdMapStore;
        _itemFactory = itemFactory;
        _tileFactory = tileFactory;
        _map = map;
    }

    public ITile TransformIntoDynamicTile(ITile tile)
    {
        if (tile is not IStaticTile staticTile) return tile;

        var itemsId = staticTile.AllClientIdItems;

        var items = new List<IItem>(itemsId.Length);

        foreach (var clientId in itemsId)
        {
            if (!_itemClientServerIdMapStore.TryGetValue(clientId, out var serverId)) continue;

            var item = _itemFactory.Create(serverId, tile.Location, new Dictionary<ItemAttribute, IConvertible>());
            items.Add(item);
        }

        var dynamicTile = _tileFactory.CreateDynamicTile(new Coordinate(tile.Location), TileFlag.None, items.ToArray());

        _map.ReplaceTile(dynamicTile);
        return dynamicTile;
    }
}