using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Contracts.Contracts;

namespace Server.Jobs.Items;

public class LiquidPoolJob
{
    public static void Execute(ILiquid item, IGameServer game)
    {
        if (item is not { Decay.Expired: true }) return;

        var tile = game.Map[item.Location] as IDynamicTile;
        if (item.Decay.TryDecay()) game.Map.CreateBloodPool(item, tile); //todo: need to review this

        if (item.Decay.ShouldDisappear) game.Map.CreateBloodPool(null, tile);
    }
}