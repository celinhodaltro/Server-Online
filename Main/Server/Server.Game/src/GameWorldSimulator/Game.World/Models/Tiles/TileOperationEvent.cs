using System;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Results;

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