using Game.Common.Contracts.Items;
using Game.Common.Location;
using Game.Common.Location.Structs;

namespace Game.Common.Contracts.World.Tiles;

public interface ITileFactory
{
    ITile CreateTile(Coordinate coordinate, TileFlag flag, IItem[] items);
    ITile CreateDynamicTile(Coordinate coordinate, TileFlag flag, IItem[] items);
}