using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Location;
using Server.Entities.Common.Location.Structs;

namespace Server.Entities.Common.Contracts.World.Tiles;

public interface ITileFactory
{
    ITile CreateTile(Coordinate coordinate, TileFlag flag, IItem[] items);
    ITile CreateDynamicTile(Coordinate coordinate, TileFlag flag, IItem[] items);
}