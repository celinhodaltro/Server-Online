using System;
using System.Collections.Generic;
using Server.Entities.Models.Contracts.DataStores;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Contracts.Services;
using Server.Entities.Models.Contracts.World;
using Server.Entities.Models.Contracts.World.Tiles;
using Server.Entities.Models.Item;
using Server.Entities.Models.Location;
using Server.Entities.Models.Location.Structs;

namespace Server.Entities.Models.World;

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