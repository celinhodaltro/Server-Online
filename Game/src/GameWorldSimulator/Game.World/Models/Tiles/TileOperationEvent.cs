using System;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.World.Tiles;
using Game.Common.Results;

namespace Game.World.Models.Tiles;

public static class TileOperationEvent
{
    public static event Action<ITile, IItem, OperationResultList<IItem>> OnTileChanged;
    public static event Action<ITile> OnTileLoaded;

    public static void OnChanged(ITile tile, IItem thing, OperationResultList<IItem> operation)
    {
        OnTileChanged?.Invoke(tile, thing, operation);
    }

    public static void OnLoaded(ITile tile)
    {
        OnTileLoaded?.Invoke(tile);
    }
}