using Game.Common.Contracts.World.Tiles;

namespace Game.Common.Contracts.Services;

public interface IStaticToDynamicTileService
{
    ITile TransformIntoDynamicTile(ITile tile);
}