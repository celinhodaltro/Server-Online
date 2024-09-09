using Server.Entities.Models.Location;
using Server.Entities.Models.Location.Structs;
using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.World.Tiles;

public interface ITileFactory
{
    ITile CreateTile(Coordinate coordinate, TileFlag flag, IItem[] items);
    ITile CreateDynamicTile(Coordinate coordinate, TileFlag flag, IItem[] items);
}