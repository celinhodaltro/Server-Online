using Server.Entities.Models.Contracts.World.Tiles;

namespace Server.Entities.Models.Contracts.Services;

public interface IStaticToDynamicTileService
{
    ITile TransformIntoDynamicTile(ITile tile);
}