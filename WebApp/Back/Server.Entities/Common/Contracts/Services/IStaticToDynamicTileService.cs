using Server.Entities.Common.Contracts.World.Tiles;

namespace Server.Entities.Common.Contracts.Services;

public interface IStaticToDynamicTileService
{
    ITile TransformIntoDynamicTile(ITile tile);
}