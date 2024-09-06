using System.Collections.Generic;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.World.Tiles;
using Game.Common.Location;
using Game.Common.Location.Structs;
using Game.Tests.Helpers;
using Game.World.Models.Tiles;

namespace Game.World.Tests;

public class TileTestFactory
{
    private static ITile CreateTile(Coordinate coord, params IItem[] item)
    {
        var topItems = new List<IItem>
        {
            ItemTestData.CreateTopItem(1, 1),
            ItemTestData.CreateTopItem(2, 2)
        };

        var items = new List<IItem>
        {
            ItemTestData.CreateRegularItem(100),
            ItemTestData.CreateRegularItem(200)
        };
        items.AddRange(item);

        var tile = new DynamicTile(coord, TileFlag.None, null, topItems.ToArray(), items.ToArray());
        return tile;
    }
}