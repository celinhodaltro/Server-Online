using System;
using System.Collections.Generic;
using Game.Common.Contracts.Items;
using Game.Common.Location;
using Game.Common.Location.Structs;
using Game.Tests.Helpers;
using Game.World.Models.Tiles;

namespace Game.World.Tests;

public class MapTest
{
    public Map.Map CreateMap(IItem item)
    {
        var world = new World();

        for (var x = 100; x < 120; x++)
        for (var y = 100; y < 120; y++)
        {
            var items = new List<IItem>
            {
                ItemTestData.CreateRegularItem(1)
            };

            if (item.Location == new Location((ushort)x, (ushort)y, 7)) items.Add(item);

            world.AddTile(new DynamicTile(new Coordinate(x, y, 7), TileFlag.None, null, Array.Empty<IItem>(),
                items.ToArray()));
        }

        return new Map.Map(world);
    }
}