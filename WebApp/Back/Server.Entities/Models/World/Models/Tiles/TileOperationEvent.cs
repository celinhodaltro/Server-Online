using System;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Contracts.World.Tiles;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.World;

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